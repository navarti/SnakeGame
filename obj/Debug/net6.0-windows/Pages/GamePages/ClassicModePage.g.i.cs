﻿#pragma checksum "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36461797836E3801E18C6535C5710FCCE0536BCB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SnakeGame.Pages.GamePages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
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


namespace SnakeGame.Pages.GamePages {
    
    
    /// <summary>
    /// ClassicModePage
    /// </summary>
    public partial class ClassicModePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton BackButton;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ScoreBlock;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border GridBorder;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.UniformGrid GameGrid;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Overlay;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock OverlayText;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SnakeGame;V1.0.0.0;component/pages/gamepages/classicmodepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
            ((SnakeGame.Pages.GamePages.ClassicModePage)(target)).PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Page_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
            ((SnakeGame.Pages.GamePages.ClassicModePage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
            ((SnakeGame.Pages.GamePages.ClassicModePage)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Page_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BackButton = ((System.Windows.Controls.RadioButton)(target));
            
            #line 31 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
            this.BackButton.Click += new System.Windows.RoutedEventHandler(this.BackButtonClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ScoreBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            
            #line 54 "..\..\..\..\..\Pages\GamePages\ClassicModePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.QuitClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.GridBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.GameGrid = ((System.Windows.Controls.Primitives.UniformGrid)(target));
            return;
            case 7:
            this.Overlay = ((System.Windows.Controls.Border)(target));
            return;
            case 8:
            this.OverlayText = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

