namespace NFCommon
{
  using System.Collections;

  public class CalibrationPoint
  {
    public short Index { get; set; }
    public short Value { get; set; }

    public CalibrationPoint()
    {
    }

    public CalibrationPoint(short index, short value)
    {
      Index = index;
      Value = value;
    }
  }

  public class CalibrationArray
  {
    private ArrayList calibrationPoints;

    public CalibrationArray(short[] indexes, short[] values)
    {
      calibrationPoints = new ArrayList();
      for (int i = 0; i < indexes.Length; i++)
      {
        calibrationPoints.Add(new CalibrationPoint(indexes[i], values[i]));
      }

      if (calibrationPoints == null)
      {
        throw new System.NullReferenceException("Constructor of CalibrationArray expects a non-null ArrayList");
      }
    }

    public short[] GetIndexes()
    {
      short[] indexes = new short[calibrationPoints.Count];
      for (int i = 0;i < calibrationPoints.Count;i++)
      {
        indexes[i] = ((CalibrationPoint)calibrationPoints[i]).Index;
      }

      return indexes;
    }

    public short[] GetValues()
    {
      short[] values = new short[calibrationPoints.Count];
      for (int i = 0; i < calibrationPoints.Count; i++)
      {
        values[i] = ((CalibrationPoint)calibrationPoints[i]).Value;
      }

      return values;
    }

    public CalibrationArray(ArrayList calibrationPoints)
    {
      if (calibrationPoints == null)
      {
        throw new System.NullReferenceException("Constructor of CalibrationArray expects a non-null ArrayList");
      }

      this.calibrationPoints = calibrationPoints;
    }

    public void AddCalibrationPoint(short index, short value)
    {
      // if we have an exact match, then update the value
      var point = ForIndex(index);
      if (point != null)
      {
        point.Value = value;
        point.Index = index;
        return;
      }

      // find where to insert the new index, so that we can keep our array sorted
      var insertionIndex = GetInsertionIndex(index);
      if (insertionIndex == -1)
      {
        calibrationPoints.Add(new CalibrationPoint(index, value));
      }
      else
      {
        calibrationPoints.Insert(insertionIndex, new CalibrationPoint(index, value));
      }
    }

    public int Count()
    {
      return calibrationPoints.Count;
    }

    public bool GetCalibrationPoint(short index, out short value, bool exact = false)
    {
      value = 0;

      if (exact == true)
      {
        var point = ForIndex(index);
        if (point == null)
        {
          return false;
        }
        else
        {
          value = point.Value;
          return true;
        }
      }

      if (calibrationPoints.Count == 0)
      {
        return false;
      }
      else if (calibrationPoints.Count == 1)
      {
        value = ((CalibrationPoint)calibrationPoints[0]).Value;
        return true;
      }
      else
      {
        int i1;
        int i2 = GetInsertionIndex(index);
        if (i2 == -1)
        {
          i2 = calibrationPoints.Count - 1;
          i1 = calibrationPoints.Count - 2;
        }
        else if (i2 == 0)
        {
          i2 = 1;
          i1 = 0;
        }
        else
        {
          i1 = i2 - 1;
        }

        var p2 = (CalibrationPoint)calibrationPoints[i2];
        var p1 = (CalibrationPoint)calibrationPoints[i1];

        float m = (p2.Value - p1.Value) / (p2.Index - p1.Index);

        float y = m * index - m * p1.Index + p1.Value;

        value = (short)y;

        return true;
      }
    }

    private int GetInsertionIndex(short newIndex)
    {
      for (int i = 0; i < calibrationPoints.Count; i++)
      {
        if (((CalibrationPoint)calibrationPoints[i]).Index > newIndex)
        {
          return i;
        }
      }

      return -1;
    }

    private CalibrationPoint ForIndex(short index)
    {
      foreach (CalibrationPoint point in calibrationPoints)
      {
        if (point.Index == index)
        {
          return point;
        }
      }

      return null;
    }
  }
}
