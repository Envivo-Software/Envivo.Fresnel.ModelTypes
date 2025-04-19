// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Used for presenting data in a chart
    /// </summary>
    public interface IChartData
    {
        public Guid Id { get; }
        public string ChartType { get; }
        public string Title { get; }
        public string XAxisLabel { get; }
        public string YAxisLabel { get; }
        public IDataSeries[] Series { get; }

        public interface IDataSeries
        {
            public Guid Id { get; }
            public string SeriesName { get; }
            public string HexColour { get; }
            public IDataPoint[] Points { get; }
        }

        public interface IDataPoint
        {
            public double XValue { get; }
            public double YValue { get; }
            public double Size { get; }
            public string PointName { get; }
            public string HexColour { get; }
        }
    }
}
