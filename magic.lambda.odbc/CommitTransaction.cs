﻿/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using magic.node;
using magic.signals.contracts;
using magic.data.common.helpers;

namespace magic.lambda.odbc
{
    /// <summary>
    /// [odbc.transaction.commit] slot for committing the top level ODBC
    /// database transaction.
    /// </summary>
    [Slot(Name = "odbc.transaction.commit")]
    public class CommitTransaction : ISlot
    {
        /// <summary>
        /// Handles the signal for the class.
        /// </summary>
        /// <param name="signaler">Signaler used to signal the slot.</param>
        /// <param name="input">Root node for invocation.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            signaler.Peek<Transaction>("odbc.transaction").Commit();
        }
    }
}
