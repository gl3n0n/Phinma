using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MiscClass
/// </summary>
public class MiscClass
{
	public MiscClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public System.Data.DataTable GetTable(System.Data.OleDb.OleDbDataReader _reader)
    {

        System.Data.DataTable _table = _reader.GetSchemaTable();
        System.Data.DataTable _dt = new System.Data.DataTable();
        System.Data.DataColumn _dc;
        System.Data.DataRow _row;
        System.Collections.ArrayList _al = new System.Collections.ArrayList();

        for (int i = 0; i < _table.Rows.Count; i++)
        {

            _dc = new System.Data.DataColumn();

            if (!_dt.Columns.Contains(_table.Rows[i]["ColumnName"].ToString()))
            {

                _dc.ColumnName = _table.Rows[i]["ColumnName"].ToString();
                _dc.Unique = Convert.ToBoolean(_table.Rows[i]["IsUnique"]);
                _dc.AllowDBNull = Convert.ToBoolean(_table.Rows[i]["AllowDBNull"]);
                _dc.ReadOnly = Convert.ToBoolean(_table.Rows[i]["IsReadOnly"]);
                _al.Add(_dc.ColumnName);
                _dt.Columns.Add(_dc);

            }

        }

        while (_reader.Read())
        {

            _row = _dt.NewRow();

            for (int i = 0; i < _al.Count; i++)
            {

                _row[((System.String)_al[i])] = _reader[(System.String)_al[i]];

            }

            _dt.Rows.Add(_row);

        }

        return _dt;

    }
}
