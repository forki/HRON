using HRONLib;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HRON
{
    /// <summary>
    /// Interaktionslogik für MasterData.xaml
    /// </summary>
    public abstract partial class MasterData : UserControl
    {
        abstract protected void UserControl_Loaded(object sender, RoutedEventArgs e);

        abstract protected void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e);

        abstract protected void BtnSave_Click(object sender, RoutedEventArgs e);

        abstract protected void grdMasterData_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e);
    }

    public class MasterData<T, TEntity> : MasterData where T : DbSet<TEntity> where TEntity : baseEntity
    {
        protected T entity;
        protected HRONEntities Context;

        public MasterData(T e, HRONEntities context)
        {
            entity = e;
            this.Context = context;
            InitializeComponent();
        }

        protected override void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Context.SaveChanges();
        }

        protected override void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // nothing to do...
        }

        protected override void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["baCarPolicyViewSource"];
                entity.Load();
                myCollectionViewSource.Source = entity.Local;
            }
        }

        protected override void grdMasterData_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)) && e.PropertyType.GetGenericArguments().Length>0)
                e.Cancel = true;
        }
    }
}
