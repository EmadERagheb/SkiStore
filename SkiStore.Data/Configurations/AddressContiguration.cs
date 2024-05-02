using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiStore.Domain.Identity;
using SkiStore.Domain.ModelLists;

namespace SkiStore.Data.Configurations
{
    public class AddressContiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData(AddressList.Addresses);
        }
    }
}
