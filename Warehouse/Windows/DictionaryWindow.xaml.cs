using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Warehouse.Views
{
    public partial class DictionaryWindow : Window
    {
        private readonly dynamic _collection;

        public DictionaryWindow(string title, dynamic collection)
        {
            InitializeComponent();
            Title = title;
            _collection = collection;
            LbxItems.ItemsSource = collection;
        }

        private void LbxItems_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DictionaryItemWindow("Добавить", "");
            dialog.Owner = this;
            if (dialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(dialog.ResultName))
            {
                dynamic item = System.Activator.CreateInstance(_collection[0].GetType())!;
                item.Name = dialog.ResultName;
                item.Id = _collection.Count + 1;
                _collection.Add(item);
                LbxItems.ItemsSource = null;
                LbxItems.ItemsSource = _collection;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LbxItems.SelectedItem == null) return;
            dynamic selected = LbxItems.SelectedItem; 
            var dialog = new DictionaryItemWindow("Изменить", (string)selected.Name);
            dialog.Owner = this;
            if (dialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(dialog.ResultName))
            {
                selected.Name = dialog.ResultName;
                LbxItems.ItemsSource = null;
                LbxItems.ItemsSource = _collection;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LbxItems.SelectedItem == null) return;
            var confirmation = MessageBox.Show("Удалить запись?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmation == MessageBoxResult.Yes)
            {
                _collection.Remove(LbxItems.SelectedItem);
                LbxItems.ItemsSource = null;
                LbxItems.ItemsSource = _collection;
            }
        }
    }
}