using System.Linq;

namespace fluentmigrator_sample
{
    public class Params
    {
        public long? DownVersion { get; set; }
    }

    public static class Helpers
    {
        /// <summary>
        /// Get parameters from arguments
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Params</returns>
        public static Params GetParams(string[] args)
        {
            var downIndex = args
                .Select((item, index) => new { Index = index, Value = item })
                .FirstOrDefault(x => x.Value == "--down")?.Index;

            return new Params()
            {
                DownVersion = downIndex is null ? null : long.Parse(args[downIndex.Value + 1])
            };
        }
    }
}
