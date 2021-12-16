/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using System.Threading.Tasks;
using magic.node;
using magic.data.common;
using magic.signals.contracts;
using magic.data.common.helpers;
using magic.lambda.odbc.helpers;

namespace magic.lambda.odbc
{
    /// <summary>
    /// [odbc.scalar] slot for executing a scalar type of SQL command.
    /// </summary>
    [Slot(Name = "odbc.scalar")]
    public class Scalar : ISlot, ISlotAsync
    {
        /// <summary>
        /// Handles the signal for the class.
        /// </summary>
        /// <param name="signaler">Signaler used to signal the slot.</param>
        /// <param name="input">Root node for invocation.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            Executor.Execute(
                input,
                signaler.Peek<OdbcConnectionWrapper>("odbc.connect").Connection,
                signaler.Peek<Transaction>("odbc.transaction"),
                (cmd, _) =>
            {
                input.Value = Converter.GetValue(cmd.ExecuteScalar());
            });
        }

        /// <summary>
        /// Handles the signal for the class.
        /// </summary>
        /// <param name="signaler">Signaler used to signal the slot.</param>
        /// <param name="input">Root node for invocation.</param>
        /// <returns>An awaitable task.</returns>
        public async Task SignalAsync(ISignaler signaler, Node input)
        {
            await Executor.ExecuteAsync(
                input,
                signaler.Peek<OdbcConnectionWrapper>("odbc.connect").Connection,
                signaler.Peek<Transaction>("odbc.transaction"),
                async (cmd, _) =>
            {
                input.Value = await cmd.ExecuteScalarAsync();
            });
        }
    }
}
