-- Active: 1723651187715@@192.168.1.10@1433@TodoApp

SELECT 
"Host","Port",Count("Host")
 FROM "Proxies"
 GROUP BY "Host","Port"
 Having Count("Host")> 1 ;
SELECT Count(*) from "Proxies"-- where "Host" = '3.9.71.167'

Update "ProxyJobs" set "TimeRun" ='300' where "TimeRun" = '18000000'

DELETE T
From (
SELECT *
, DupRank = ROW_NUMBER() OVER (
              PARTITION BY "Host","ProxyType"
              ORDER BY (SELECT NULL)
            )
FROM "Proxies"
)AS T
WHERE DupRank > 1 


SELECT Count(Url) from "TikiProductLinks"
--GROUP BY Url
--Having COUNT(Url) > 1 