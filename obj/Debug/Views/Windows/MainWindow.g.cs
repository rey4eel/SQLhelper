﻿#pragma checksum "..\..\..\..\Views\Windows\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "43A1C3E70E9F57CE1E68E56E2E42BCAB490BFB8A6FC7E3337E9BC1BC73E5CFB0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FontAwesome5;
using FontAwesome5.Converters;
using KYSQLhelper;
using KYSQLhelper.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace KYSQLhelper {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KYSQLhelper.MainWindow App;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SqlConnectBtn;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxDB;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxTable;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FromColumnSelectedPrime;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxSelectSecond;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxSelectThird;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxCompareType;
        
        #line default
        #line hidden
        
        
        #line 175 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxSort;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxOrder;
        
        #line default
        #line hidden
        
        
        #line 201 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 217 "..\..\..\..\Views\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ClearTable;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/KYSQLhelper;component/views/windows/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Windows\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.App = ((KYSQLhelper.MainWindow)(target));
            return;
            case 2:
            this.SqlConnectBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.ComboBoxDB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.ComboBoxTable = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.FromColumnSelectedPrime = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.ComboBoxSelectSecond = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.ComboBoxSelectThird = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.ComboBoxCompareType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.ComboBoxSort = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.ComboBoxOrder = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 12:
            this.ClearTable = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

