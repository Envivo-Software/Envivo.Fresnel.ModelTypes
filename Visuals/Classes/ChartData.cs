// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System;

namespace Envivo.Fresnel.ModelTypes.Visuals.Classes
{
    /// <inheritdoc cref="IChartData" />
    public class ChartData : IChartData
    {
        // <inheritdoc/>
        public Guid Id { get; set; }

        // <inheritdoc/>
        public string ChartType { get; set; }

        // <inheritdoc/>
        public string Title { get; set; }

        // <inheritdoc/>
        public string XAxisLabel { get; set; }

        // <inheritdoc/>
        public string YAxisLabel { get; set; }

        public IChartData.IDataSeries[] Series { get; set; } = [];


        /// <inheritdoc cref="IChartData.IDataSeries" />
        public class DataSeries : IChartData.IDataSeries
        {
            // <inheritdoc/>
            public Guid Id { get; set; }

            // <inheritdoc/>
            public string SeriesName { get; set; }

            // <inheritdoc/>
            public string HexColour { get; set; }

            // <inheritdoc/>
            public IChartData.IDataPoint[] Points { get; set; } = [];
        }

        /// <inheritdoc cref="IChartData.IDataPoint" />
        public class DataPoint : IChartData.IDataPoint
        {
            // <inheritdoc/>
            public Guid Id { get; set; }

            // <inheritdoc/>
            public double XValue { get; set; }

            // <inheritdoc/>
            public double YValue { get; set; }

            // <inheritdoc/>
            public double Size { get; set; }

            // <inheritdoc/>
            public string PointName { get; set; }

            // <inheritdoc/>
            public string HexColour { get; set; }
        }
    }
}
