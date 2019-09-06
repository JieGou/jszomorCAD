﻿using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using EquipmentPosition;
using OrganiCAD.AutoCAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jszomorCAD
{
  public class InsertBlockTable
  {
    #region
    //public void InsertVfdPump(Database db, PromptIntegerResult numberOfItem, PromptIntegerResult distance, string blockName, string layerName, int eqIndex) => 
    //  InsertBlockTableMethod(db, numberOfItem, distance, blockName, layerName, "Centrifugal Pump", eqIndex); // todo: magic numberOfItem

    public void InsertBlockTableMethodAsTable(Database db, InsertBlockBase insertData)
      => InsertBlockTableMethod(db, insertData);

    public void InsertBlockTableMethodAsVisibility(Database db, InsertBlockBase insertData)
      => InsertBlockTableMethod(db, insertData);

    public void InsertBlockTableMethodAsPipe(Database db, InsertBlockBase insertData)
      => InsertBlockTableMethod(db, insertData);


    //public void InsertBlockTableBaseMethod(Database db, InsertBlockBase insertData)
    //{
    //  // 1. which block to inster? insertData.BlockName
    //  // get the block to insert
    //}
    #endregion

    public void InsertBlockTableMethod(Database db, InsertBlockBase insertData)
    {
      var sizeProperty = new PositionProperty();
      sizeProperty.FreeSpace = 60;

      Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;     
      var aw = new AutoCadWrapper();
      BlockTableRecord btr;
      var blockDefinitions = new List<ObjectId>();
      var positionProperty = new PositionProperty();

      //setup default layers
      var defultLayers = new LayerCreator();
      defultLayers.Layers();
    
      //var shortEqIndex = Convert.ToInt16(eqIndex);

      // Start transaction to insert equipment
      aw.ExecuteActionOnBlockTable(db, (tr, bt) =>
      {        
        foreach (ObjectId btrId in bt)
        {
          using (btr = (BlockTableRecord)tr.GetObject(btrId, OpenMode.ForRead, false))
          {
            // Only add named & non-layout blocks to the copy list
            if (!btr.IsAnonymous && !btr.IsLayout && btr.Name == insertData.BlockName)
            {  
              blockDefinitions.Add(btrId);             
            }
          }
        }        

        var currentSpaceId = tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

        for (int i = 0; i < insertData.NumberOfItem; i++)
        {
          foreach (var objectId in blockDefinitions)
          {
            using (var blockDefinition = (BlockTableRecord)tr.GetObject(objectId, OpenMode.ForRead, false))
            {
              using (var acBlkRef = new BlockReference(new Point3d(insertData.X, insertData.Y, positionProperty.Z), objectId))
              {
                currentSpaceId.AppendEntity(acBlkRef);
                tr.AddNewlyCreatedDBObject(acBlkRef, true);

                acBlkRef.Layer = insertData.LayerName;                

                // copy/create attribute references
                foreach (var bdEntityObjectId in blockDefinition)
                {
                  var ad = tr.GetObject(bdEntityObjectId, OpenMode.ForRead) as AttributeDefinition;
                  if (ad == null) continue;

                  var ar = new AttributeReference();
                  ar.SetDatabaseDefaults(db);
                  ar.SetAttributeFromBlock(ad, acBlkRef.BlockTransform);
                  ar.TextString = ad.TextString;
                  ar.AdjustAlignment(HostApplicationServices.WorkingDatabase);

                  acBlkRef.AttributeCollection.AppendAttribute(ar);
                  tr.AddNewlyCreatedDBObject(ar, true);
                
                  System.Diagnostics.Debug.Print($"Tag={ar.Tag} TextString={ar.TextString}");

                  if (acBlkRef.IsDynamicBlock)
                  {
                    foreach (DynamicBlockReferenceProperty dbrProp in acBlkRef.DynamicBlockReferencePropertyCollection) // this loop must be here
                                                                                                                        //else tag rotation for pump will be wrong!
                    {
                      if (dbrProp.PropertyName == insertData.PropertyName)
                        dbrProp.Value = insertData.EqIndex; // SHORT !!!!!!!!!!!!

                      // for jet pump rotate
                      if (ar.TextString == "Air Jet Pump")                      
                        acBlkRef.Rotation = DegreeHelper.DegreeToRadian(270); // this command must be here else tag rotation will be wrong!
                    }
                  }

                  //text for EQ tank - Attributes
                  if (ar.Tag == "NAME1" && insertData.BlockName == "chamber")
                    ar.TextString = "EQUALIZATION";
                  if (ar.Tag == "NAME2" && insertData.BlockName == "chamber")
                    ar.TextString = "TANK";

                  //valve setup
                  if (insertData.BlockName == "valve")
                  {
                    //ar.Rotation = DegreeHelper.DegreeToRadian(90);
                    acBlkRef.Rotation = DegreeHelper.DegreeToRadian(90);
                  }
                }

                // setup item by index
                #region
                //if (acBlkRef.IsDynamicBlock)
                //{
                //  foreach (DynamicBl ockReferenceProperty dbrProp in acBlkRef.DynamicBlockReferencePropertyCollection)
                //  {       
                //    if (dbrProp.PropertyName == PropertyName)                                    
                //      dbrProp.Value = eqIndex; // SHORT !!!!!!!!!!!!                                                           
                //  }
                //}
                #endregion

                // udpate attribute reference values after setting the visibility state or block table index
                foreach (ObjectId arObjectId in acBlkRef.AttributeCollection)
                {
                  foreach (DynamicBlockReferenceProperty dbrProp in acBlkRef.DynamicBlockReferencePropertyCollection)
                  {
                    var ar = arObjectId.GetObject<AttributeReference>();
                    if (ar == null) continue;

                    if (dbrProp.PropertyName == "PipeLength")
                      dbrProp.Value = insertData.PipeLength;

                    // for jet pump tag position
                    if (ar.Tag == "NOTE" && ar.TextString == "Air Jet Pump" && insertData.BlockName == "pump")
                    {
                      //acBlkRef.Rotation = DegreeHelper.DegreeToRadian(270); // this command has a wrong result that is why should be in the upper loop.

                      if (acBlkRef.IsDynamicBlock)
                      {
                        // tag horizontal positioning
                        if (dbrProp.PropertyName == "Angle")
                          dbrProp.Value = DegreeHelper.DegreeToRadian(90);
                        if (dbrProp.PropertyName == "Position X")
                          dbrProp.Value = (double)6;
                        if (dbrProp.PropertyName == "Position Y")
                          dbrProp.Value = (double)0;
                      }
                    }
                    //for pumps VFD rotate
                    if (dbrProp.PropertyName == "Angle1" && ar.TextString == "Equalization Tank Pump")
                      dbrProp.Value = DegreeHelper.DegreeToRadian(90);

                    // pumps VFD rotate
                    if (dbrProp.PropertyName == "Angle2" && ar.TextString == "Equalization Tank Pump")
                      dbrProp.Value = DegreeHelper.DegreeToRadian(270);

                    //setup chamber width
                    if (dbrProp.PropertyName == "Distance" && insertData.BlockName == "chamber")
                      dbrProp.Value = PositionProperty.NumberOfPump * PositionProperty.DistanceOfPump + sizeProperty.FreeSpace; //last value is the free space for other items
                    //text position for chamber
                    if (dbrProp.PropertyName == "Position X" && insertData.BlockName == "chamber")
                      dbrProp.Value = ((PositionProperty.NumberOfPump * PositionProperty.DistanceOfPump + sizeProperty.FreeSpace) / (double)2); //place text middle of chamber horizontaly 
                  }
                }
              }
            }
          }
          insertData.X += insertData.Distance;
        }
        currentSpaceId.UpdateAnonymousBlocks();
      });
    }
  } 
}