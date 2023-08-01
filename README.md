# end

Hacker News API Client is an API which has one method that via talks to the HAcker API to get the best stories and then converts it to local format and returns the topN records. If no count is requested, it sets a default of 50 records.




This could be tested using the link http://localhost:5149/api/hackernews/5 to get top 5 records.

Given time, we should avoid lot of records being returned in one go, we can look at asynchrnous streaming etc.
and also put rules on input values etc
