import seaborn as sns
df = sns.load_dataset('penguins')
df=df.dropna() # 결측값 제거