// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Rules.Classes;
using Envivo.Fresnel.ModelTypes.Structural;
using System.Threading;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Temporal.ProcessFlow
{
    /// <summary>
    /// A unit of work within a stage, which can either be executed directly (if it contains no tasks) or broken down into parallel tasks.
    /// </summary>
    public interface IStep : IEntity
    {
        /// <summary>
        /// Audit trail recording who created, updated, and deleted this step.
        /// </summary>
        IAudit Audit { get; }

        /// <summary>
        /// The order in which this step appears within its stage.
        /// Steps are processed in ascending sequence order.
        /// </summary>
        int SequenceOrder { get; }

        /// <summary>
        /// The friendly name for this stage.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A human-readable description of this step.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The current status of this step. 
        /// </summary>
        StepState CurrentState { get; }

        /// <summary>
        /// Checks whether this step (or all of its tasks) is ready to be completed.
        /// </summary>
        Task<Assertion> ValidateAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks this step as completed, recording that the work for this step is finished.
        /// Returns a result indicating whether the completion was successful or if there are outstanding issues.
        /// </summary>
        Task<Assertion> CompleteAsync(CancellationToken cancellationToken = default);
    }
}