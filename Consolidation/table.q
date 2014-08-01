
// Generate mock up data.
dateMap:()!();

days:{x + 2014.07.01} each til 30;

getRandTimeOfDate:{[date] date + 00:00:00.000 + rand 3600 * 1000 * 24 };

createTableOfDate:{[date;amount]
	flip (`sym                 ;`user        ;`time)!
		 (`$(string til amount);amount#`hugog;getRandTimeOfDate each amount#date)
 };
 
{ dateMap[x]:createTableOfDate[x;10000] } each days;

today:{ dateMap[.z.D] };
yesterday:{ dateMap[.z.D-1] };

// Problem resolution.
timeCount:{[grand]
	`int$((1440 % grand) + 1)
 };

times:{[grand]
	00:00 + grand * til timeCount grand
 };

emptySub:{[grand;colName]
	flip (`minute     ;colName)!
	     (times[grand];(timeCount grand)#0)
 };
 
getSubmissionCount:{[t;grand]
	colName:(string (first t)[`time])[5 + til 5];
	(enlist `minute) xkey
	emptySub[grand;`$colName]
	lj
	(`minute;`$colName) xcol
	select count 1 by grand xbar time.minute from t
 };

gradu:1

todaySub:{ getSubmissionCount[today;gradu] };
yesterdaySub:{ getSubmissionCount[yesterday;gradu] };
monthSub:{x,'y} over { getSubmissionCount[dateMap[x];gradu] } each days;
monthAvg: (enlist `minute) xkey
          flip (`minute;`avg)!(times[gradu];{ avg monthSub[x] } each times[gradu]);

monthCur:sums monthSub;
monthAvgCur:sums monthAvg;
