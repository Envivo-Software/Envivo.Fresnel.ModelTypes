// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Rules.Classes;
using Envivo.Fresnel.ModelTypes.Structural;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Temporal.ProcessFlow
{
    /// <summary>
    /// A phase of the business process, which can either be executed directly (if it contains no steps) or broken down into smaller steps.
    /// </summary>
    public interface IStage : IEntity
    {
        /// <summary>
        /// The order in which this stage appears in the overall process.
        /// Stages are processed in ascending sequence order.
        /// </summary>
        int SequenceOrder { get; }

        /// <summary>
        /// The friendly name for this stage.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A human-readable description of this stage.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Optional. The collection of steps within this stage.
        /// </summary>
        IReadOnlyCollection<IStep> Steps { get; }

        /// <summary>
        /// The current status of this stage. 
        /// </summary>
        StageState CurrentState { get; }

        /// <summary>
        /// Audit trail recording who created, updated, and deleted this stage.
        /// </summary>
        IAudit Audit { get; }

        /// <summary>
        /// Checks whether this stage (or all of its steps) is ready to be completed.
        /// </summary>
        public async Task<Assertion> ValidateAsync(CancellationToken cancellationToken = default)
        {
            if (Steps == null || Steps.Count == 0)
                return Assertion.Pass;

            var validationTasks = Steps.Select(s => s.ValidateAsync(cancellationToken));
            var allAssertions = await Task.WhenAll(validationTasks);

            var result = allAssertions.Consolidate();
            return result;
        }

        /// <summary>
        /// Marks this stage as completed, recording that this phase is finished. 
        /// </summary>
        public async Task<Assertion> CompleteAsync(CancellationToken cancellationToken = default)
        {
            if (Steps == null || Steps.Count == 0)
                return Assertion.Pass;

            var completionTasks = Steps.Select(s => s.CompleteAsync(cancellationToken));
            var allAssertions = await Task.WhenAll(completionTasks);

            var result = allAssertions.Consolidate();
            return result;
        }
    }
}