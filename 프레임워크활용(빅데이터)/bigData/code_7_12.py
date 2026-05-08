import seaborn as sns


df = sns.load_dataset('tips')
df.head()


p1 = df.pivot_table(index = 'sex', columns = 'day', values = 'total_bill', aggfunc = 'mean')
p1.head()

p2 = df.pivot_table(index = 'sex', columns='time', values='total_bill', aggfunc='mean')
p2.head()

p3 = df.pivot_table(index ='time', columns = 'day', values = 'tip', aggfunc = 'mean')
p3.head()