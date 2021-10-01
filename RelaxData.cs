using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberMethods
{
    public class RelaxData
    {
        System.Data.DataSet dataSet;
        public DataTable dt = new DataTable($"Таблиця ітерацій");
        public DataColumn column;
        public DataRow row;


        public RelaxData()
        {
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "i";
            column.ReadOnly = false;
            column.Unique = false;
            column.Caption = "i";

            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "delta 1";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "delta 2";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);
                column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "delta 3";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "x1";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "x2";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "x3";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);


            dt.Columns[0].SetOrdinal(0);
            //column = new DataColumn();
            //column.DataType = System.Type.GetType("System.Double");
            //column.ColumnName = "y";
            //column.AutoIncrement = false;
            //column.Caption = "y";
            //column.ReadOnly = false;
            //column.Unique = false;

            //dt.Columns.Add(column);

            //DataColumn[] PrimaryKeyColumns = new DataColumn[0];
            //PrimaryKeyColumns[0] = dt.Columns["n"];
            //dt.PrimaryKey = PrimaryKeyColumns;

            dataSet = new DataSet();
            dataSet.Tables.Add(dt);

        }
    }
}
