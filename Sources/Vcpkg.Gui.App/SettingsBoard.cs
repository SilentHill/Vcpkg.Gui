using Avalonia.Controls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpkg.Gui.App
{
    public class SettingsBoard : UserControl
    {
        public SettingsBoard()
        {
            _dataTable = _createPropertyDataTable();
            _dataGrid = _createPropertyDataGrid();
            //_dataGrid.Items = _dataTable;
        }

        private DataTable _createPropertyDataTable()
        {




            var p = new System.Diagnostics.Process();
            p.StartInfo.CreateNoWindow = false;













            var dataTable  = new DataTable();

            return dataTable;
        }
        private DataTable _dataTable { get; }

        private DataGrid _createPropertyDataGrid()
        {
            var dataGrid = new DataGrid();
            dataGrid.AutoGenerateColumns = true;
            return dataGrid;
        }
        private DataGrid _dataGrid { get; }
    }


}
