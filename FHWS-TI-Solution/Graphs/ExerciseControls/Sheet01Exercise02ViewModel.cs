﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Utils;

namespace Graphs.ExerciseControls
{
    class Sheet01Exercise02ViewModel : ExerciseViewModelBase
    {
        public ValueWraper<bool?> EulerianPathCheckResult { get; } = new ValueWraper<bool?>(null);
        public ValueWraper<bool?> EulerianCircuitCheckResult { get; } = new ValueWraper<bool?>(null);
        public RelayCommand CheckForEulerianPathsCommand { get; }
        public RelayCommand CheckForEulerianCircuitsCommand { get; }

        public RelayCommand<double> AnimateBreadthFirstSearchCommand { get; }
        public RelayCommand<double> AnimateDepthFirstSearchCommand { get; }

        public ValueWraper<bool?> CheckForCyclesResult { get; } = new ValueWraper<bool?>(null);
        public RelayCommand CheckForCyclesCommand { get; }

        public Sheet01Exercise02ViewModel()
        {
            CheckForEulerianPathsCommand = new RelayCommand(
                () => EulerianPathCheckResult.Value = _graph.HasEulerianPath(),
                () => _graph != null);

            CheckForEulerianCircuitsCommand = new RelayCommand(
                () => EulerianCircuitCheckResult.Value = _graph.HasEulerianCircuit(),
                () => _graph != null);

            AnimateBreadthFirstSearchCommand = new RelayCommand<double>(
                speed => _graph.WalkThroughBreadthFirstSearch((int)speed),
                d => _graph != null);

            AnimateDepthFirstSearchCommand = new RelayCommand<double>(
                speed => _graph.WalkThroughDepthFirstSearch((int)speed),
                d => _graph != null);

            CheckForCyclesCommand = new RelayCommand(
                () => CheckForCyclesResult.Value = _graph.IsCyclic(),
                () => _graph != null);
        }

        protected override void OnGraphUpdated()
        {
            EulerianPathCheckResult.Value = null;
            EulerianCircuitCheckResult.Value = null;
            CheckForCyclesResult.Value = null;
        }

    }
}
