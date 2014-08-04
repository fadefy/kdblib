// Generate mock up data.
dateMap:()!();
days: 2014.07.01 + til 31;
getRandTimeOfDate:{[date]
 date + 00:00:00.000 + rand 3600 * 1000 * 24 };
createTable:{[amount;values]
 flip (`sym;`user;`time)!(`$(string til amount);amount#`hugog;values) };
createTableOfDate:{[date;amount]
 createTable[amount;getRandTimeOfDate each amount#date] }; 
createTableOfDateEven:{[date;amount]
 createTable[amount;amount#(date + 12:00:00.000)] };
// No data in 29
{ dateMap[x]:createTableOfDate[x;10000 + rand 1000] } each days[til 28];
// Exotic on 2014.7.31
dateMap[2014.07.31]:createTableOfDate[2014.07.31;15000];
// Even on 2014.7.30
dateMap[2014.07.30]:createTableOfDateEven[2014.07.30;10000];
show "GenerationComplete";

// Problem resolution.
timeCount:{[grand]
 `int$(1440 % grand) };
times:{[grand]
 00:00 + grand * til timeCount grand };
emptySub:{[grand]
 flip (`minute;`val)!(times[grand];(timeCount grand)#0) };
getSubCount:{[table;grand]
 emptySub[grand] lj select val:count 1 by grand xbar time.minute from table };

gradu:1
toMMDD:{[date]
 time:string date; `$(time[5 + til 2], time[8 + til 2]) };
getSubCountOfDay:{[grand;day] (`minute;toMMDD[day]) xcol getSubCount[dateMap[day];grand] }
getMonthSub:{[grand] {x,'y} over getSubCountOfDay[grand] each days };
monthSub:getMonthSub[gradu];
monthAggValue:{[f;days;time]
 f monthSub[time;days] };
monthAggOfDays:{[f;days]
 flip (`minute;`avg)!(times[gradu]; monthAggValue[f;days] each times[gradu]) };

monthAvg:monthAggOfDays[avg;toMMDD each days];
diffWeight:(til timeCount[gradu]) % (sum til timeCount[gradu]);
getSubmissionDiff:{[date]
 (sums monthAvg[`avg]) - (sums monthSub[toMMDD[date]]) };
daysDev: days ! { sum diffWeight * {x * x} getSubmissionDiff[x] } each days;
selectiveDays: where daysDev < 80 * (med daysDev);
monthSelectiveAvg:monthAggOfDays[avg;toMMDD each selectiveDays];

monthCur:{[g] sums getMonthSub[g]};
monthAvgCur:sums monthSelectiveAvg;
