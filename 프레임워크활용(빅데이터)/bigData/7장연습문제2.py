import seaborn as sns
df = sns.load_dataset('penguins')
df = df.dropna() #결측값 제거

df

# 팽귄의 종을 기준으로 부리의 가로 길이와 세로 길이의 평균을 출력하시오.
mean_bill_length = df.groupby('species')['bill_length_mm'].mean()
mean_bill_depth = df.groupby('species')['bill_depth_mm'].mean()
mean_bill_length
mean_bill_depth

#팽귄의 성별을 기준으로 몸무게와 낼개의 길이의 최대값을 출력하시오.
max_body_mass = df.groupby('sex')['body_mass_g'].max()
max_bill_length = df.groupby('sex')['bill_length_mm'].max()
max_body_mass
max_bill_length

#팽권의 종과 성별을 깆누으로 부리의 가로길이와 세로길이의 평균을 출력하시오.
mean_bill_length = df.groupby(['species', 'sex'])['bill_length_mm'].mean()
mean_bill_depth = df.groupby(['species', 'sex'])['bill_depth_mm'].mean()
mean_bill_length    
mean_bill_depth


#팽귄의 종과 성별을 기준으로 빈도수를 집계하여 출력하시오.
species_sex_counts = df.groupby(['species', 'sex']).size()
print(species_sex_counts)

#팽귄의 종과 서식지를 기준으로 평균 날개 길이를 집계하여 출력하시오.
mean_flipper_length = df.groupby(['species', 'island'])['flipper_length_mm'].mean()
mean_flipper_length

#펭귄의 종과 성별을 기준으로 평균 몸무게를 집계하여 출력하시오.
mean_body_mass = df.groupby(['species', 'sex'])['body_mass_g'].mean()
mean_body_mass