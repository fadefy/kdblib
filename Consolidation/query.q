h:hopen localhost:5000;

// Simply
days: h `days;

// With arguments to call a function on the other side.
result:h (each[{dateMap[x]}];days);

// Bit complex.
monthSub:h({[d] {x,'y} over getSubCountOfDay[1] each d};days);

myGetSubCount:{[table;grand]
 emptySub[grand] lj select val:count 1 by grand xbar time.minute from table };
myGetSubCountOfDay:{[grand;day] (`minute;toMMDD[day]) xcol getSubCount[dateMap[day];grand] }

monthSubOfMyOwn:h({[f] {x,'y} over f[1] each }[myGetSubCountOfDay];days);