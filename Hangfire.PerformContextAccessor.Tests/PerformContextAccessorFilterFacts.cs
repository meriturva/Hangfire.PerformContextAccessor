using Hangfire.Common;
using Hangfire.PerformContextAccessor.Tests.Mocks;
using Hangfire.Server;
using Xunit;

namespace Hangfire.PerformContextAccessor.Tests
{
    public class PerformContextAccessorFilterFacts
    {
        private readonly PerformContextMock _context;
        private readonly PerformContextAccessor _performContextAccessor;

        public PerformContextAccessorFilterFacts()
        {
            _context = new PerformContextMock();
            _performContextAccessor = new PerformContextAccessor();
        }

        [Fact]
        public void OnPerforming_ThrowsArgumentNullException_WhenContextIsNull()
        {
            var filter = CreateFilter();

            var exception = Assert.Throws<ArgumentNullException>(() => filter.OnPerforming(null));

            Assert.Equal("context", exception.ParamName);
        }

        [Fact]
        public void OnPerforming_Assign_PerformContext()
        {
            var filter = CreateFilter();

            filter.OnPerforming(CreatePerformingContext());

            Assert.Equal("JobId", _performContextAccessor.PerformingContext.BackgroundJob.Id);
        }

        private PerformContextAccessorFilter CreateFilter()
        {
            return new PerformContextAccessorFilter(_performContextAccessor);
        }

        private PerformingContext CreatePerformingContext()
        {
            return new PerformingContext(_context.Object);
        }
    }
}
