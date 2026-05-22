import seaborn as sns
import matplotlib.pyplot as plt

df = sns.load_dataset('flights')
df.head()


df = df.pivot_table(index='month', columns='year', values='passengers', aggfunc='mean')
df.head()

sns.set_theme(rc={'figure.figsize':(12,12)})
sns.heatmap(df).set_title('heatmap of Flight Passengers', fontsize=20)
plt.show()