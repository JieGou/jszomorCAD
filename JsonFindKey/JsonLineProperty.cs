﻿using Autodesk.AutoCAD.Geometry;
using JsonParse;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonFindKey
{
  public class JsonClassProperty
  {
    public JsonLineProperty jsonLineProperty { get; set; } = new JsonLineProperty();
    public JsonBlockProperty jsonBlockProperty { get; set; } = new JsonBlockProperty();

  }

  public class JsonLineProperty
  {
    public string Type { get; set; }
    public string ObjectId { get; set; }
    //public Coordinate Start { get; set; } = new Coordinate();
    //public Coordinate End { get; set; } = new Coordinate();
    public List<Point2D> LinePoints { get; set; } = new List<Point2D>();
    //public Point2D Point2D { get; set; } = new Point2D();
  }

  public class Coordinate
  {
    public double X {get;set;}
    public double Y { get; set; }
  }

  public class Point2D
  {

    private const int _digits = 4;
    public const double distanceError = 0.0001d;//double.Epsilon;
    public const double angleError = 0.00001d;//double.Epsilon;

    public double X { get; set; }

    public double Y { get; set; }

    public string Name;

    public int Point;

    //public decimal RoundedX
    //{
    //  get
    //  {
    //    return Convert.ToDecimal(Math.Round(X, _digits));
    //  }
    //}
    //public decimal RoundedY
    //{
    //  get
    //  {
    //    return Convert.ToDecimal(Math.Round(Y, _digits));
    //  }
    //}

    public Point2D()
    {

    }
    public Point2D(double x, double y, int point)
    {
      X = x;
      Y = y;
      Point = point;
    }
  }
}
