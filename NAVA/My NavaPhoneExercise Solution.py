# 
# Your previous Plain Text content is preserved below:
# 
# /**
# You are helping launch a new system that will require migrating user data from the legacy system. We will need to prepare the exported data by transforming it into JSON.
# 
# Your solution should be a single function, in any language, that:
# - Takes one argument: an input string containing a list of csv-formatted records
# - Returns a string: the outputted JSON-formatted string, one per line
# - You may assume all user data is valid. You do not need to handle unexpected fields or data types.
# 
# These are the fields:
# 
# +------------------------+---------------+-----------------+
# | Name                   | Data type     | Example value   |
# +------------------------+---------------+-----------------+
# | id                     | number        | 9737452         |
# | username               | string        | johndoe27       |
# | email                  | string        | bob@yahoo.com   |
# | level of authorization | String        | 'unconfirmed',  |
# |                        |               | 'confirmed', or |
# |                        |               | 'id proofed'    |
# | date of birth          | number        | 19540528        |
# +------------------------+---------------+-----------------+
# 
# A sample legacy csv input might look like this:
# 
# 9737452,johndoe27,rockstar_dad@yahoo.com,id proofed,19540528\n2941846,young4eva,eva47@aol.com,unconfirmed,19790129
# 
# Note the newline character (/n) separating these two distinct user records.
# 
# The new system expects each user to be a JSON user object. Output for the above would look like:
#     
# {"username": "johndoe27", "email": "rockstar_data@yahoo.com", "levelOfAuthorization": 2, "dateOfBirth": 19540528}
# {"username": "young4eva", "email": "eva47@aol.com", "levelOfAuthorization": 0, "dateOfBirth": 19790129}
# 
# Notes:
# - The id field is ignored.
# - When mapping levelOfAuthorization from CSV -> JSON, convert:
#   "unconfirmed" => 0
#   "confirmed" => 1
#   "id proofed" => 2
# */

given_str = "johndoe27,rockstar_dad@yahoo.com,id proofed,19540528\n2941846,young4eva,eva47@aol.com,unconfirmed,19790129\n9737452,johndoe27,rockstar_dad@yahoo.com,id proofed,19540528\n2941846,young4eva,eva47@aol.com,unconfirmed,19790129\n9737452,johndoe27,rockstar_dad@yahoo.com,id proofed,19540528\n2941846,young4eva,eva47@aol.com,unconfirmed,19790129"
list_values = given_str.replace('\n', ',').replace('unconfirmed', '0').replace('confirmed', '1').replace('id proofed', '2').split(',')
list_keys = [ 'username', 'email', 'levelOfAuthorization', 'dateOfBirth']


def UserDataDictionary(keyList, valueList):
	i = 0
	temp = valueList;
	while len(temp) > 0:
		temp = valueList[5*i:]
		print dict(zip(keyList, temp))
		i += 1

UserDataDictionary(list_keys, list_values)