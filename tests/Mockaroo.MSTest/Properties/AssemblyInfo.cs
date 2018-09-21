using Acklann.Diffa;
using Acklann.Diffa.Reporters;

[assembly: SaveFilesAt("approved-results")]
[assembly: Use(typeof(BeyondCompare4Reporter))]