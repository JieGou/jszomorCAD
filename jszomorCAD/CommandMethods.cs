﻿using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using OrganiCAD.AutoCAD;
using System;
using System.Linq;
using System.Collections.Generic;
using EquipmentPosition;

namespace jszomorCAD
{
  public class CommandMethods
  {
    public string path = @"E:\Test\Autocad PID blocks work in progress.dwg";    

    [CommandMethod("jcad_EqTankBuilder")]
    public void ListBlocks()
    {
      var db = Application.DocumentManager.MdiActiveDocument.Database;

      // Copy blocks from sourcefile into opened file
      var copyBlockTable = new CopyBlockTable();
      var btrNamesToCopy = new[] { "pump", "valve", "chamber", "instrumentation tag", "channel gate", "pipe" };
      copyBlockTable.CopyBlockTableMethod(db, path, btr =>
      {
        System.Diagnostics.Debug.Print(btr.Name);
        return btrNamesToCopy.Contains(btr.Name);
      });
     
      var blocks = new[]
      {
         new InsertBlockBase(numberOfItem: 1,
         blockName: "chamber",
         layerName: "unit",
         propertyName: "Visibility",
         visibilityStateIndex: "no channel",
         x: 0,
         y: 0,
         hostName: "Equalization Tank"),
      };

      var layers = new[]
      {
        new LayerData("unit", Color.FromRgb(255, 0, 0), 0.25)
      };

      var layerCreator = new LayerCreator();
      layerCreator.LayerCreatorMethod(layers);

      DrawBlocks(db, blocks);
      

      //var sizeProperty = new PositionProperty();
      ////"\nEnter number of equipment:"
      //var pio = new PromptIntegerOptions("\nEnter number of equipment:") { DefaultValue = 5 };
      //var number = Application.DocumentManager.MdiActiveDocument.Editor.GetInteger(pio);

      ////"\nEnter distance of equipment:"
      //var dio = new PromptIntegerOptions("\nEnter distance of equipment:") { DefaultValue = 20 };
      //var distance = Application.DocumentManager.MdiActiveDocument.Editor.GetInteger(dio);

      ////"\nEnter index of equipment:"
      //var eio = new PromptIntegerOptions("\nEnter index of equipment:") { DefaultValue = 22 };
      //var eqIndex = Application.DocumentManager.MdiActiveDocument.Editor.GetInteger(eio);

      //var shortEqIndex = Convert.ToInt16(eqIndex.Value);
      ////short shortCheckValveIndex = 2;
      //PositionProperty.NumberOfPump = number.Value;
      //PositionProperty.DistanceOfPump = distance.Value;

      //var ed = Application.DocumentManager.MdiActiveDocument.Editor;
      //var aw = new AutoCadWrapper();


      ////copyBlockTable.CopyBlockTableMethod(db, path, btr => true);
      //sizeProperty.X = 50;

      //// Call a transaction to create layer

      ////layerCreator.LayerCreatorMethod("equipment", Color.FromRgb(0, 0, 255), 0.5);

      ////layerCreator.LayerCreatorMethod("unit", Color.FromRgb(0, 0, 255), 0.5);

      //// Start transaction to write equipment

      //var insertEqTAnkPump = new InsertBlockBase(PositionProperty.NumberOfPump,               // number of item
      //  PositionProperty.DistanceOfPump,             // disctance of item
      //  "pump",                                      //block name
      //  "equipment",                                 //layer name
      //  "Centrifugal Pump",                          //dynamic property type
      //  shortEqIndex,                                //visibility of equipment ()
      //  sizeProperty.X,                              //X
      //  10,                                          //Y
      //  "Equalization Tank",                         //Host name       
      //  0);                                          //pipe length
      //insertBlockTable.InsertBlockTableMethod(db, insertEqTAnkPump);
      ////
      //var insertEqTAnk = new InsertBlockBase(
      //  1,                                          // number of item
      //  0,                                          // disctance of item
      //  "chamber",                                  //block name
      //  "unit",                                     //layer name
      //  "Visibility",                               //dynamic property type
      //  "no channel",                               //visibility of equipment
      //  0,                                          //X
      //  0,                                          //Y
      //  "Equalization Tank",                        //Host name       
      //  0);                                         //pipe length
      //insertBlockTable.InsertBlockTableMethod(db, insertEqTAnk);
      ////
      //var insertCheckValve = new InsertBlockBase(
      //  PositionProperty.NumberOfPump,              // number of item
      //  PositionProperty.DistanceOfPump,            // disctance of item
      //  "valve",                                    //block name
      //  "valve",                                    //layer name
      //  "Block Table1",                             //dynamic property type
      //  (short)5,                                          //visibility of equipment (check valve)
      //  sizeProperty.X,                             //X
      //  25,                                         //Y
      //  "Equalization Tank",                        //Host name       
      //  0);                                         //pipe length
      //insertBlockTable.InsertBlockTableMethod(db, insertCheckValve);
      ////
      //var insertGateValve = new InsertBlockBase(
      //  PositionProperty.NumberOfPump,            // number of item
      //  PositionProperty.DistanceOfPump,          // disctance of item
      //  "valve",                                  //block name
      //  "valve",                                  //layer name
      //  "Block Table1",                           //dynamic property type
      //  (short)0,                                        //visibility of equipment (gate valve)
      //  sizeProperty.X,                           //X
      //  40,                                       //Y
      //  "Equalization Tank",                      //Host name       
      //  0);                                       //pipe length
      //insertBlockTable.InsertBlockTableMethod(db, insertGateValve);
      ////
      //var insertLIT = new InsertBlockBase(
      //  1,                                        // number of item
      //  0,                                        // disctance of item
      //  "instrumentation tag",                    //block name
      //  "instrumentation",                        //layer name
      //  "Block Table1",                           //dynamic property type
      //  (short)7,                                        //visibility of equipment (LIT)
      //  PositionProperty.NumberOfPump
      //  * PositionProperty.DistanceOfPump + 50,   //X
      //  10,                                       //Y
      //  "Equalization Tank",                      //Host name       
      //  0);                                       //pipe length
      ////
      //var insertFIT = new InsertBlockBase(
      //  1,                                        // number of item
      //  0,                                        // disctance of item
      //  "instrumentation tag",                    //block name
      //  "instrumentation",                        //layer name
      //  "Block Table1",                           //dynamic property type
      //  (short)11,                                       //visibility of equipment (FIT)
      //  PositionProperty.NumberOfPump
      //  * PositionProperty.DistanceOfPump + 50,   //X
      //  50,                                       //Y
      // "Equalization Tank",                       //Host name       
      //  0);                                       //pipe length
      ////
      //var insertJetPump = new InsertBlockBase(
      //  1,                                        // number of item
      //  0,                                        // disctance of item
      //  "pump",                                   //block name
      //  "equipment",                              //layer name
      //  "Centrifugal Pump",                       //dynamic property type
      //  (short)17,                                       //visibility of jet pump
      //  20,                                       //X
      //  10,                                       //Y
      //  "Equalization Tank",                      //Host name       
      //  0);                                       //pipe length
      ////
      //var insertChannelGateSTB = new InsertBlockBase(
      //  1,                                        // number of item
      //  0,                                        // disctance of item
      //  "channel gate",                           //block name
      //  "equipment",                              //layer name
      //  "Block Table1",                           //dynamic property type
      //  (short)23,                                       //visibility of item (Channel gate (Equalization Tank))
      //  -10.5,                                    //X
      //  6,                                        //Y
      // "Equalization Tank",                       //Host name       
      //  0);                                       //pipe length
      ////

      //var insertChannelGateDTY = new InsertBlockBase(
      //  1,                                         // number of item
      //  0,                                         // disctance of item
      //  "channel gate",                            //block name
      //  "equipment",                               //layer name
      //  "Block Table1",                            //dynamic property type
      //  (short)23,                                        //visibility of item (Channel gate (Equalization Tank))
      //  -10.5,                                     //X
      //  -24,                                       //Y
      //  "Equalization Tank",                       //Host name       
      //  0);                                        //pipe length

      //var insertPipe1 = new InsertBlockBase(
      //  PositionProperty.NumberOfPump,             // number of item
      //  PositionProperty.DistanceOfPump,           // disctance of item
      //  "pipe",                                    //block name
      //  "sewer",                                   //layer name
      //  "Visibility1",                             //dynamic property type
      //  "sewer",                                   //visibility of item (sewer pipe)
      //  50,                                        //X
      //  14,                                        //Y
      //  "Equalization Tank",                       //Host name       
      //  36);

      //var blocks = new[] {
      //  insertPipe1, insertChannelGateDTY, insertChannelGateSTB
      //};




      MoveToBottom.SendToBackWipeout();
      MoveToBottom.SendToBackLine();
      SendClass.SendRegen();
      SendClass.SendZoomExtents();
    } 

    public void DrawBlocks(Database db, IEnumerable<InsertBlockBase> blocks)
    {
      var insertBlockTable = new InsertBlockTable(db);

      foreach (var block in blocks)
      {
        insertBlockTable.InsertBlock(block);

      }
    }
  }
}

