﻿using JsonFindKey;
using JsonParse;
using System.Collections.Generic;

namespace jCAD.Test
{
	public class DeepEx
	{
		public List<object> Comments { get; set; } = new List<object>();
		public bool LineTypeComparer(JsonLineProperty line1, JsonLineProperty line2)
		{
			//System.Diagnostics.Debug.WriteLine($"AutoCAD TAG: {attRef.Tag}");
			var properties1 = line1.Type;//.GetType().GetProperties();
			var properties2 = line2.Type;//.GetType().GetProperties();
			if (properties1.Length != properties2.Length ||
					properties1.ToString() != properties2.ToString())
			{
				if (!Comments.Contains($"InternalId: {line1.Internal_Id}"))
				{
					Comments.Add($"InternalId: {line1.Internal_Id}");
				}
				Comments.Add(($"Type: {properties1}"));
				return false;
			}
			return true;
		}

		public bool LinePointsComparer(JsonLineProperty line1, JsonLineProperty line2)
		{
			if (line1.LinePoints.Count != line2.LinePoints.Count)
			{
				Comments.Add($"LinePoint number is not equal at Id: {line1.Internal_Id}");
				return false;
			}

			for (int j = 0; j < line1.LinePoints.Count; j++)
			{
				var properties1 = line1.LinePoints[j].GetType().GetProperties();
				var properties2 = line2.LinePoints[j].GetType().GetProperties();
				if (properties1.Length != properties2.Length) return false;

				for (int i = 0; i < properties1.Length; i++)
				{

					var customAttributes = properties1[i]
						 .GetCustomAttributes(typeof(Newtonsoft.Json.JsonPropertyAttribute), false);
					if (customAttributes.Length == 1)
					{
						var jsonProp = customAttributes[0];
						var jsonTagName = (jsonProp as Newtonsoft.Json.JsonPropertyAttribute).PropertyName;
						//System.Diagnostics.Debug.WriteLine($"\tJSONProperty Name: {jsonTagName}");
						if (jsonTagName != null)
						{
							var propValue1 = properties1[i].GetValue(line1.LinePoints[j]);
							var propValue2 = properties2[i].GetValue(line2.LinePoints[j]);
							if (propValue1 == null || propValue2 == null) continue;
							if (!string.IsNullOrWhiteSpace(propValue1.ToString()) ||
									!string.IsNullOrWhiteSpace(propValue2.ToString()))
							{
								if (propValue1.ToString() != propValue2.ToString())
								{
									if(!Comments.Contains($"InternalId: {line1.Internal_Id}"))
									{
										Comments.Add($"InternalId: {line1.Internal_Id}");
									}
									Comments.Add($"Point: {line1.LinePoints[j].Point}");
									Comments.Add($"{jsonTagName}: {propValue1}");
									//return false;
								}
							}
						}
					}
				}
			}
			return true;
		}
		public bool BlockGeometryCompare(JsonBlockProperty block1, JsonBlockProperty block2)
		{
			//System.Diagnostics.Debug.WriteLine($"AutoCAD TAG: {attRef.Tag}");
			var properties1 = block1.Geometry.GetType().GetProperties();
			var properties2 = block2.Geometry.GetType().GetProperties();
			if (properties1.Length != properties2.Length) return false;
			for (int i = 0; i < properties1.Length; i++)
			{
				var customAttributes = properties1[i]
					 .GetCustomAttributes(typeof(Newtonsoft.Json.JsonPropertyAttribute), false);
				if (customAttributes.Length == 1)
				{
					var jsonProp = customAttributes[0];
					var jsonTagName = (jsonProp as Newtonsoft.Json.JsonPropertyAttribute).PropertyName;
					//System.Diagnostics.Debug.WriteLine($"\tJSONProperty Name: {jsonTagName}");
					if (jsonTagName != null)
					{
						var propValue1 = properties1[i].GetValue(block1.Geometry);
						var propValue2 = properties2[i].GetValue(block2.Geometry);
						if (propValue1 == null || propValue2 == null) continue;
						if (!string.IsNullOrWhiteSpace(propValue1.ToString()) || !string.IsNullOrWhiteSpace(propValue2.ToString()))
						{
							if (propValue1.ToString() != propValue2.ToString())
							{
								if (!Comments.Contains($"InternalId: {block1.Attributes.Internal_Id}"))
								{
									Comments.Add($"InternalId: {block1.Attributes.Internal_Id}");
								}
								Comments.Add($"Geometry Category-{jsonTagName}: {propValue1}");
								return false;
							}
						}
					}
				}
			}
			return true;
		}
		public bool BlockMiscCompare(JsonBlockProperty block1, JsonBlockProperty block2)
		{
			//System.Diagnostics.Debug.WriteLine($"AutoCAD TAG: {attRef.Tag}");
			var properties1 = block1.Misc.GetType().GetProperties();
			var properties2 = block2.Misc.GetType().GetProperties();
			if (properties1.Length != properties2.Length) return false;
			for (int i = 0; i < properties1.Length; i++)
			{
				var customAttributes = properties1[i]
					 .GetCustomAttributes(typeof(Newtonsoft.Json.JsonPropertyAttribute), false);
				if (customAttributes.Length == 1)
				{
					var jsonProp = customAttributes[0];
					var jsonTagName = (jsonProp as Newtonsoft.Json.JsonPropertyAttribute).PropertyName;
					//System.Diagnostics.Debug.WriteLine($"\tJSONProperty Name: {jsonTagName}");
					if (jsonTagName != null)
					{
						var propValue1 = properties1[i].GetValue(block1.Misc);
						var propValue2 = properties2[i].GetValue(block2.Misc);
						if (propValue1 == null || propValue2 == null) continue;
						if (!string.IsNullOrWhiteSpace(propValue1.ToString()) || !string.IsNullOrWhiteSpace(propValue2.ToString()))
						{
							if (propValue1.ToString() != propValue2.ToString())
							{
								if (!Comments.Contains($"InternalId: {block1.Attributes.Internal_Id}"))
								{
									Comments.Add($"InternalId: {block1.Attributes.Internal_Id}");
								}
								Comments.Add($"{jsonTagName}: {propValue1}");
								return false;
							}
						}
					}
				}
			}
			return true;
		}
		public bool BlockGeneralCompare(JsonBlockProperty block1, JsonBlockProperty block2)
		{
			//System.Diagnostics.Debug.WriteLine($"AutoCAD TAG: {attRef.Tag}");
			var properties1 = block1.General.GetType().GetProperties();
			var properties2 = block2.General.GetType().GetProperties();
			if (properties1.Length != properties2.Length) return false;
			for (int i = 0; i < properties1.Length; i++)
			{
				var customAttributes = properties1[i]
					 .GetCustomAttributes(typeof(Newtonsoft.Json.JsonPropertyAttribute), false);
				if (customAttributes.Length == 1)
				{
					var jsonProp = customAttributes[0];
					var jsonTagName = (jsonProp as Newtonsoft.Json.JsonPropertyAttribute).PropertyName;
					//System.Diagnostics.Debug.WriteLine($"\tJSONProperty Name: {jsonTagName}");
					if (jsonTagName != null)
					{
						var propValue1 = properties1[i].GetValue(block1.General);
						var propValue2 = properties2[i].GetValue(block2.General);
						if (propValue1 == null || propValue2 == null) continue;
						if (!string.IsNullOrWhiteSpace(propValue1.ToString()) || !string.IsNullOrWhiteSpace(propValue2.ToString()))
						{
							if (propValue1.ToString() != propValue2.ToString())
							{
								if (!Comments.Contains($"InternalId: {block1.Attributes.Internal_Id}"))
								{
									Comments.Add($"InternalId: {block1.Attributes.Internal_Id}");
								}
								Comments.Add($"{jsonTagName}: {propValue1}");
								return false;
							}
						}
					}
				}
			}
			return true;
		}
		public bool BlockCustomCompare(JsonBlockProperty block1, JsonBlockProperty block2)
		{
			//System.Diagnostics.Debug.WriteLine($"AutoCAD TAG: {attRef.Tag}");
			var properties1 = block1.Custom.GetType().GetProperties();
			var properties2 = block2.Custom.GetType().GetProperties();
			if (properties1.Length != properties2.Length) return false;
			for (int i = 0; i < properties1.Length; i++)
			{
				var customAttributes = properties1[i]
					 .GetCustomAttributes(typeof(Newtonsoft.Json.JsonPropertyAttribute), false);
				if (customAttributes.Length == 1)
				{
					var jsonProp = customAttributes[0];
					var jsonTagName = (jsonProp as Newtonsoft.Json.JsonPropertyAttribute).PropertyName;
					//System.Diagnostics.Debug.WriteLine($"\tJSONProperty Name: {jsonTagName}");
					if (jsonTagName != null)
					{
						var propValue1 = properties1[i].GetValue(block1.Custom);
						var propValue2 = properties2[i].GetValue(block2.Custom);
						if (propValue1 == null || propValue2 == null) continue;
						if (!string.IsNullOrWhiteSpace(propValue1.ToString()) || !string.IsNullOrWhiteSpace(propValue2.ToString()))
						{
							if (propValue1.ToString() != propValue2.ToString())
							{
								if (!Comments.Contains($"InternalId: {block1.Attributes.Internal_Id}"))
								{
									Comments.Add($"InternalId: {block1.Attributes.Internal_Id}");
								}
								Comments.Add($"Custom Category-{jsonTagName}: {propValue1}");
								return false;
							}
						}
					}
				}
			}
			return true;
		}
		public bool BlockAttributesCompare(JsonBlockProperty block1, JsonBlockProperty block2)
		{
			//System.Diagnostics.Debug.WriteLine($"AutoCAD TAG: {attRef.Tag}");
			var properties1 = block1.Attributes.GetType().GetProperties();
			var properties2 = block2.Attributes.GetType().GetProperties();
			if (properties1.Length != properties2.Length) return false;
			for (int i = 0; i < properties1.Length; i++)
			{
				var customAttributes = properties1[i]
					 .GetCustomAttributes(typeof(Newtonsoft.Json.JsonPropertyAttribute), false);
				if (customAttributes.Length == 1)
				{
					var jsonProp = customAttributes[0];
					var jsonTagName = (jsonProp as Newtonsoft.Json.JsonPropertyAttribute).PropertyName;
					//System.Diagnostics.Debug.WriteLine($"\tJSONProperty Name: {jsonTagName}");
					if (jsonTagName != null)
					{
						var propValue1 = properties1[i].GetValue(block1.Attributes);
						var propValue2 = properties2[i].GetValue(block2.Attributes);
						if (propValue1 == null || propValue2 == null) continue;
						if (!string.IsNullOrWhiteSpace(propValue1.ToString()) || !string.IsNullOrWhiteSpace(propValue2.ToString()))
						{
							if (propValue1.ToString() != propValue2.ToString())
							{
								if (!Comments.Contains($"InternalId: {block1.Attributes.Internal_Id}"))
								{
									Comments.Add($"InternalId: {block1.Attributes.Internal_Id}");
								}
								Comments.Add($"{jsonTagName}: {propValue1}");
								return false;
							}
						}
					}
				}
			}
			return true;
		}
	}
}
