using FluentValidation.Results;
using MShop.Core.Exception;
using MShop.Core.Message;

namespace MShop.Catalog.UnitTests.Domain.Entity.Category
{
    public class CategoryTest : CategoryTestFixture
    {

        private readonly Notifications _notifications;

        public CategoryTest()
        {
            _notifications = new Notifications();
        }

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category")]
        public void Instantiate()
        {
            var valid = GetCategoryValid();
            var guid = valid.Id;

            var category = GetCategoryValid(valid.Id, valid.Name, valid.IsActive);
            var result = category.IsValid();

            Assert.True(result.IsValid);
            Assert.NotNull(category);
            Assert.Equal(valid.Name, category.Name);
            Assert.Equal(valid.IsActive, category.IsActive);
            Assert.Equal(guid, category.Id);

        }

        [Theory(DisplayName = nameof(SholdReturnErrorWhenNameIsInvalid))]
        [Trait("Domain", "Category")]
        [MemberData(nameof(ListNamesCategoryInvalid))]
        public void SholdReturnErrorWhenNameIsInvalid(string? name)
        {
            var category = GetCategoryValid(Guid.NewGuid(), name);
            ValidationResult result = category.IsValid();

            Assert.False(result.IsValid);
            Assert.True(result.Errors.Any());
        }

        [Fact(DisplayName = nameof(SholdReturnErrorWhenIdEmpty))]
        [Trait("Domain", "Category")]
        public void SholdReturnErrorWhenIdEmpty()
        {
            var category = GetCategoryValid(Guid.Empty, "CategoryTest");
            var result = category.IsValid();

            Assert.False(result.IsValid);
            Assert.True(result.Errors.Any());

        }

    }
}
