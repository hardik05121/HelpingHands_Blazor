redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51N0pIDSGwyHHZ7b7k4sai2SxzksSea4PdW1aB4WtQ8q3UNh0aHbqAnOcYjzLNVFG8c7XVoFeRNEbk7WjLkaOhmam00Lselcqxk");
    stripe.redirectToCheckout({ sessionId: sessionId });
}