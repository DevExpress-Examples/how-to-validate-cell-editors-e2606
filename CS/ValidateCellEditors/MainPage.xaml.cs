using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ValidateCellEditors {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
            grid.DataSource = new ProductList();
        }

        private void GridColumn_Validate(object sender, DevExpress.Xpf.Grid.GridCellValidationEventArgs e) {
            int quantity = ((Product)e.Row).Quantity;
            if (quantity < 3) {
                double discount = 100 - (Convert.ToDouble(e.Value) * 100) /
                                              Convert.ToDouble(e.CellValue);
                if (!(e.IsValid = discount > 0 && discount <= 30)) {
                    e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    if (discount < 0) {
                        e.ErrorContent = string.Format("The price cannot be greater than ${0}",
                                                    Convert.ToDouble(e.CellValue));
                        return;
                    }
                    e.ErrorContent = string.Format(
                       "The discount cannot be greater than 30% (${0}). Please correct the price.",
                       Convert.ToDouble(e.CellValue) * 0.7);
                }
            }
        }

        private void TableView_HiddenEditor(object sender, DevExpress.Xpf.Grid.EditorEventArgs e) {
            if (e.Column.FieldName != "Quantity") return;
            grid.View.CommitEditing();
        }
    }
}
