﻿using Contpaqi.Sdk.Ejemplos.Messages;
using Contpaqi.Sdk.Ejemplos.ViewModels.Conceptos;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace Contpaqi.Sdk.Ejemplos.Views.Conceptos
{
    /// <summary>
    ///     Interaction logic for ListadoConceptosView.xaml
    /// </summary>
    public partial class ListadoConceptosView
    {
        public ListadoConceptosView()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<ListadoConceptosViewModel>();
            WeakReferenceMessenger.Default.Register<ViewModelVisibilityChangedMessage>(this, (recipient, message) =>
            {
                if (message.Sender == ViewModel && message.IsOpen == false)
                {
                    Close();
                }
            });
        }

        public ListadoConceptosViewModel ViewModel => (ListadoConceptosViewModel) DataContext;
    }
}