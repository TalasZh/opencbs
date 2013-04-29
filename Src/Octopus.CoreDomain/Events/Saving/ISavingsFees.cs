using Octopus.Shared;

namespace Octopus.CoreDomain.Events.Saving
{
    public interface ISavingsFees
    {
        OCurrency Fee { get; set; }
    }
}
