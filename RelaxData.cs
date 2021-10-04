using System.Data;

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
            column.ColumnName = "x1";
            column.ReadOnly = false;
            column.Unique = false;
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "delta 1";
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
            column.ColumnName = "delta 2";
            column.ReadOnly = false;
            column.Unique = false;
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "x3";
            column.ReadOnly = false;
            column.Unique = false;
            dt.Columns.Add(column);



            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "delta 3";
            column.ReadOnly = false;
            column.Unique = false;
            dt.Columns.Add(column);
            dt.Columns[0].SetOrdinal(0);

            dataSet = new DataSet();
            dataSet.Tables.Add(dt);

        }
    }
}
