﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Graphs.Annotations;
using Graphs.ExerciseControls;
using Graphs.Utils;
using Microsoft.Win32;

namespace Graphs
{
    class MainWindowViewModel : PropertyChangedBase
    {
        private static Dictionary<string, ViewModelBase> _exerciseModelDict = new Dictionary<string, ViewModelBase>
        {
            {"Blatt 1, Aufgabe 2", new Sheet01Exercise02ViewModel()}
        };

        public List<string> ExerciseNames { get; } = _exerciseModelDict.Keys.ToList();

        public RelayCommand BrowseCommand { get; }

        public RelayCommand<string> ParseCommand { get; }
        
        public bool IsDirected { get; set; }


        public ViewModelBase CurExerciseControl
        {
            get { return _curExerciseControl; }
            set { _curExerciseControl = value; OnNotifyPropertyChanged(); }
        }

        public string SelectedExercise
        {
            get { return _selectedExercise; }
            set
            {
                if (_selectedExercise == value) return;
                _selectedExercise = value;
                CurExerciseControl = _exerciseModelDict[_selectedExercise];
            }
        }

        public string FilePath
        {
            set { _filePath = value; OnNotifyPropertyChanged(); }
            get { return _filePath; }
        }

        public Graph<VertexBase> Graph
        {
            get { return _graph; }
            private set { _graph = value; OnNotifyPropertyChanged(); }
        }


        private string _selectedExercise;
        private string _filePath;
        private Graph<VertexBase> _graph;
        private ViewModelBase _curExerciseControl;

        public MainWindowViewModel()
        {
            ExerciseNames.Sort();

            SelectedExercise = ExerciseNames.First();

            BrowseCommand = new RelayCommand(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                    FilePath = openFileDialog.FileName;
            });

            ParseCommand = new RelayCommand<string>(filePath =>
            {
                Graph = FileParser.ParseFileToGraph(filePath, IsDirected);

                Console.WriteLine($"HasEulerianPath: {Graph.HasEulerianPath()}");
                Console.WriteLine($"HasEulerianCircuit: {Graph.HasEulerianCircuit()}");
            });
        }
    }
}
