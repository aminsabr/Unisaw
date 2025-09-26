using System;
using System.Collections.Generic;
using System.Linq;

namespace Unisaw.CLI.Commands
{
    public sealed class CommandRegistry
    {
        private readonly Dictionary<string, ICommand> _byKey;

        public CommandRegistry(IEnumerable<ICommand> commands)
        {
            _byKey = new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase);
            foreach (var cmd in commands)
            {
                _byKey[cmd.Name] = cmd;
                foreach (var alias in cmd.Aliases)
                {
                    _byKey[alias] = cmd;
                }
            }
        }

        public bool TryGet(string key, out ICommand command)
            => _byKey.TryGetValue(key, out command!);

        public IEnumerable<string> Keys
            => _byKey.Keys.Distinct(StringComparer.OrdinalIgnoreCase);
    }
}
