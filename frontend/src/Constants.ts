const prod = {
	url: {
		API_URL: '',
		CONTRACTOR_API_URL: 'https://webapo20210110092719.azurewebsites.net/api'
	}
}
const dev = {
	url: {
		API_URL: 'https://localhost:5001',
		CONTRACTOR_API_URL: 'https://webapo20210110092719.azurewebsites.net/api'
	}
}
export const config = process.env.NODE_ENV === 'development' ? dev : prod