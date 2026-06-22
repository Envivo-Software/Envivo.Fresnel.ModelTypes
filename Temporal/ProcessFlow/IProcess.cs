// SPDX-FileCopyrightText: Copyright (c) 2022-2026 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Rules.Classes;
using Envivo.Fresnel.ModelTypes.Structural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Temporal.ProcessFlow
{
    /// <summary>
    /// A business process that guides users through a series of stages to achieve a specific operational outcome.
    /// </summary>
    public interface IProcess : IAggregateRoot
    {
        /// <summary>
        /// Audit trail recording who created, updated, and deleted this process.
        /// </summary>
        IAudit Audit { get; }

        /// <summary>
        /// The friendly name for this stage.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// A human-readable description of this process, used for identification.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The collection of stages that make up this process, to be completed in the order specified by each stage's sequence number.
        /// </summary>
        IReadOnlyCollection<IStage> Stages { get; }

        /// <summary>
        /// The stage currently being worked on. This can be navigated to manually by the user, saved, and restored when the process is reloaded.
        /// </summary>
        IStage CurrentStage { get; set; }

        /// <summary>
        /// Advances the process to the next incomplete stage.
        /// </summary>
        Task<Assertion> AdvanceToNextStageAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Moves the process to the given stage.
        /// </summary>
        Task<Assertion> MoveToStageAsync(IStage stage, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks all of the Stages and Steps to check for completeness.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Assertion> ValidateAllAsync(CancellationToken cancellationToken = default)
        {
            if (Stages == null || Stages.Count == 0)
                return Assertion.Fail(new ApplicationException("There are no Stages in this Process"));

            var validationTasks = Stages.Select(s => s.ValidateAsync(cancellationToken));
            var allAssertions = await Task.WhenAll(validationTasks);

            var result = allAssertions.Consolidate();
            return result;
        }

        /// <summary>
        /// Completes the entire process and creates all necessary records
        /// and notifications based on the work performed.
        /// </summary>
        Task<Assertion> FinishProcessAsync(CancellationToken cancellationToken = default);
    }
}