using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SampleSource.Domain;
namespace SampleSource
{
    public partial class Lists
    {
        ListsAndGridsViewModel viewModel = new ListsAndGridsViewModel();
        public Lists()
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Debug.WriteLine(btn.Name);
            bool sel = btn.Name.Contains("Sel");
            string target = btn.Name.Replace("btn_m", "").Substring(0, 1);
            // モデルの「Items1 / Items2」を取得する
            Type t_viewModel = typeof(ListsAndGridsViewModel);
            ObservableCollection<SelectableViewModel> val_Items
                = (ObservableCollection<SelectableViewModel>)t_viewModel.InvokeMember("Items" + target,
                    BindingFlags.GetProperty,
                    null,
                    viewModel,
                    null);
            // 選択、解除処理
            foreach (SelectableViewModel v in val_Items)
            {
                v.IsSelected = sel;
            }

            // 値をセットする
            t_viewModel.InvokeMember("ShowSet" + target,
            BindingFlags.SetProperty,
            null,
            viewModel,
            new object[] { "Name：" + btn.Name + " & Target：" + "ShowSet" + target + " Val Set & Get!" });

            string val_ShowSet = (string)t_viewModel.InvokeMember("ShowSet" + target,
                    BindingFlags.GetProperty,
                    null,
                    viewModel,
                    null);

            txt_ShowSet.Text = val_ShowSet;
        }
    }
}
