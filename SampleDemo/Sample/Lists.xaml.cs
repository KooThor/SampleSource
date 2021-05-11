using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SampleSource.Domain;
namespace SampleSource
{
    public partial class Lists
    {
        ListsViewModel viewModel = new ListsViewModel();
        public Lists()
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        #region evnet
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Debug.WriteLine(btn.Name);
            bool sel = btn.Name.Contains("Sel");
            string target = btn.Name.Replace("btn_m", "").Substring(0, 1);
            // モデルの「Items1 / Items2」を取得する
            Type t_viewModel = typeof(ListsViewModel);
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
        }

        private void btn_Click_ChkBox(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Type t_chk = typeof(CheckBox);
            bool sel = btn.Name.Contains("Sel");
            string target = btn.Name.Replace("btn_m", "").Substring(0, 1) == "3" ? "yobi" : "nichi";
            foreach (CheckBox chk in FindVisualChildren<CheckBox>(this))
            {
                if(chk.Name.Contains(target))
                 {
                    // 値をセットする
                    t_chk.InvokeMember("IsChecked",
                    BindingFlags.SetProperty,
                    null,
                    chk,
                    new object[] { sel });
                }
            }
        }
        #endregion event

        #region private method
        /// <summary>
        /// 画面項目を取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        #endregion private method
    }
}
