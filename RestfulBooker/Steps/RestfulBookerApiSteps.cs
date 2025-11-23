using System.Threading.Tasks;
using RestSharp;
using TechTalk.SpecFlow;
using SauceDemo.BddFramework.Api.Clients;
using SauceDemo.BddFramework.Api.Models;
using System.Net.Http;

namespace SauceDemo.BddFramework.RestfulBooke.Steps
{
    [Binding]
    public class RestfulBookerApiSteps
    {
        private readonly RestfulBookerClient _client;
        private RestResponse _pingResponse;
        private Booking _bookingRequest;
        private CreateBookingResponse _createBookingResponse;
        private Booking _bookingFromGet;

        public RestfulBookerApiSteps()
        {
            _client = new RestfulBookerClient();
        }

        [When(@"I send a ping request to the Restful Booker API")]
        public async Task WhenISendAPingRequestToTheRestfulBookerApi()
        {
            var httpResponse = await _client.PingAsync();
            _pingResponse = new RestResponse
            {
                StatusCode = httpResponse.StatusCode
            };
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            NUnit.Framework.Assert.That((int)_pingResponse.StatusCode, NUnit.Framework.Is.EqualTo(statusCode));
        }

        [Given(@"I have a valid booking payload")]
        public void GivenIHaveAValidBookingPayload()
        {
            _bookingRequest = new Booking
            {
                firstname = "Gustavo",
                lastname = "Clarinda",
                totalprice = 150,
                depositpaid = true,
                bookingdates = new BookingDates
                {
                    checkin = "2025-12-01",
                    checkout = "2025-12-05"
                },
                additionalneeds = "Breakfast"
            };
        }

        [When(@"I create a booking in the Restful Booker API")]
        public async Task WhenICreateABookingInTheRestfulBookerApi()
        {
            _createBookingResponse = await _client.CreateBookingAsync(_bookingRequest);
        }

        [Then(@"the booking should be successfully created")]
        public void ThenTheBookingShouldBeSuccessfullyCreated()
        {
            NUnit.Framework.Assert.That(_createBookingResponse.bookingid, NUnit.Framework.Is.GreaterThan(0));
            NUnit.Framework.Assert.That(_createBookingResponse.booking, NUnit.Framework.Is.Not.Null);
            NUnit.Framework.Assert.That(_createBookingResponse.booking.firstname, NUnit.Framework.Is.EqualTo(_bookingRequest.firstname));
            NUnit.Framework.Assert.That(_createBookingResponse.booking.lastname, NUnit.Framework.Is.EqualTo(_bookingRequest.lastname));
        }

        [Then(@"when I retrieve the created booking the returned data should be correct")]
        public async Task ThenWhenIRetrieveTheCreatedBookingTheReturnedDataShouldBeCorrect()
        {
            _bookingFromGet = await _client.GetBookingAsync(_createBookingResponse.bookingid);

            NUnit.Framework.Assert.That(_bookingFromGet.firstname, NUnit.Framework.Is.EqualTo(_bookingRequest.firstname));
            NUnit.Framework.Assert.That(_bookingFromGet.lastname, NUnit.Framework.Is.EqualTo(_bookingRequest.lastname));
            NUnit.Framework.Assert.That(_bookingFromGet.totalprice, NUnit.Framework.Is.EqualTo(_bookingRequest.totalprice));
            NUnit.Framework.Assert.That(_bookingFromGet.depositpaid, NUnit.Framework.Is.EqualTo(_bookingRequest.depositpaid));
            NUnit.Framework.Assert.That(_bookingFromGet.bookingdates.checkin, NUnit.Framework.Is.EqualTo(_bookingRequest.bookingdates.checkin));
            NUnit.Framework.Assert.That(_bookingFromGet.bookingdates.checkout, NUnit.Framework.Is.EqualTo(_bookingRequest.bookingdates.checkout));
            NUnit.Framework.Assert.That(_bookingFromGet.additionalneeds, NUnit.Framework.Is.EqualTo(_bookingRequest.additionalneeds));
        }
    }
}
