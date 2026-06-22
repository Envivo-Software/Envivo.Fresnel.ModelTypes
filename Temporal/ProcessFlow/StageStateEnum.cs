// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Temporal.ProcessFlow
{
    /// <summary>
    /// The possible states of a process Stage.
    /// </summary>
    public enum StageState
    {
        /// <summary>
        /// No work has been started on this stage yet.
        /// </summary>
        Pending,

        /// <summary>
        /// Work has been started on this stage.
        /// </summary>
        InProgress,

        /// <summary>
        /// This stage has been reviewed and meets all requirements.
        /// It is now ready to be formally completed.
        /// </summary>
        Validated,

        /// <summary>
        /// This stage has been successfully completed.
        /// All required work for this phase is done.
        /// </summary>
        Completed,

        /// <summary>
        /// This stage could not be completed due to an issue.
        /// The problem needs to be resolved before proceeding.
        /// </summary>
        Failed
    }

}