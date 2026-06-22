// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Temporal.ProcessFlow
{
    /// <summary>
    /// The possible states of a Step within a stage.
    /// </summary>
    public enum StepState
    {
        /// <summary>
        /// No work has been started on this step yet.
        /// </summary>
        Pending,

        /// <summary>
        /// Work has been started on this step.
        /// </summary>
        InProgress,

        /// <summary>
        /// This step has been reviewed and meets all requirements.
        /// It is now ready to be formally completed.
        /// </summary>
        Validated,

        /// <summary>
        /// This step has been successfully completed.
        /// All required work for this step is done.
        /// </summary>
        Completed,

        /// <summary>
        /// This step could not be completed due to an issue.
        /// The problem needs to be resolved before proceeding.
        /// </summary>
        Failed
    }

}