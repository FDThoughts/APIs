using System;
using System.Collections.Generic;

namespace CSVMultiFormat.Models
{
    /// <summary>
    /// Data model
    /// </summary>
    public class DataModel : IEquatable<DataModel>
    {
        /// <summary>
        /// Header row
        /// </summary>
        public Row header { get; set; }

        /// <summary>
        /// List of rows
        /// </summary>
        public List<Row> rows { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DataModel()
        {
            this.rows = new List<Row>();
        }

        /// <summary>
        /// Override the 'Equals' operator
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>bool - true if objects are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DataModel)obj);
        }

        /// <summary>
        /// Override GetHashCode
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Override the 'Equals' operator
        /// </summary>
        /// <param name="dm">DataModel</param>
        /// <returns>bool - true if objects are equal, false otherwise</returns>
        public bool Equals(DataModel dm)
        {
            if (ReferenceEquals(null, dm)) return false;
            if (ReferenceEquals(this, dm)) return true;
            if (!(ReferenceEquals(null, dm.header) &&
                ReferenceEquals(this.header, dm.header)))
            {
                if (!this.header.Equals(dm.header)) return false;
            }
            if (ReferenceEquals(null, dm.rows) && 
                ReferenceEquals(this.rows, dm.rows)) return true;
            if (this.rows.Count != dm.rows.Count) return false;
            for (int i = 0; i < this.rows.Count; i++)
            {
                if (!this.rows[i].Equals(dm.rows[i])) return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Row model
    /// </summary>
    public class Row : IEquatable<Row>
    {
        /// <summary>
        /// Columns list
        /// </summary>
        public List<Column> columns { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Row()
        {
            this.columns = new List<Column>();
        }

        /// <summary>
        /// Override the 'Equals' operator
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>bool - true if objects are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Row)obj);
        }

        /// <summary>
        /// Override GetHashCode
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Override the 'Equals' operator
        /// </summary>
        /// <param name="r">Row</param>
        /// <returns>bool - true if objects are equal, false otherwise</returns>
        public bool Equals(Row r)
        {
            if (ReferenceEquals(null, r)) return false;
            if (ReferenceEquals(this, r)) return true;
            if (ReferenceEquals(null, r.columns) && 
                ReferenceEquals(this.columns, r.columns)) return true;
            if (this.columns.Count != r.columns.Count) return false;
            for (int i = 0; i < this.columns.Count; i++)
            {
                if (!this.columns[i].Equals(r.columns[i])) return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Column model
    /// </summary>
    public class Column : IEquatable<Column>
    {
        /// <summary>
        /// Column Value
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value"></param>
        public Column(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Override the 'Equals' operator
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>bool - true if objects are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Column)obj);
        }

        /// <summary>
        /// Override GetHashCode
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Override the 'Equals' operator
        /// </summary>
        /// <param name="col">Column</param>
        /// <returns>bool - true if objects are equal, false otherwise</returns>
        public bool Equals(Column col)
        {
            if (ReferenceEquals(null, col)) return false;
            if (ReferenceEquals(this, col)) return true;
            return this.Value == col.Value;
        }
    }
}
