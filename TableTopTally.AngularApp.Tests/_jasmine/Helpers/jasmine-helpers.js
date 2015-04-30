var testCase = function (testCases, fn)
{
    /// <summary>
    ///     NUnit [TestCase] equivalent
    /// </summary>
    /// <param name="testCases" type="array">
    ///     The values to be tested.
    ///     If a value is an array, the values of that array will be used as a set of arguments
    /// 
    ///     Example:
    ///     If testCases equals [['foo', 'bar'], ['hello', 'world']]
    ///     This would result in the following calls:
    ///         fn('foo', 'bar')
    ///         fn('hello', 'world')
    /// </param>
    /// <param name="fn" type="function">The function to call with each value</param>

    for (var i = 0; i < testCases.length; i++)
    {
        (function (testCase)
        {
            if (Array.isArray(testCase))
            {
                fn.apply(null, testCase);
            }
            else
            {
                fn.call(null, testCase);
            }
        })(testCases[i]);
    }
};