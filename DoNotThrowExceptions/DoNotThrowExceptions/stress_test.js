import http from 'k6/http';
import { check } from 'k6';

export const options = {
    vus: 10,
    duration: '1m',
    insecureSkipTlsVerify: true
}
export default () => {
    const url ='http://localhost:5060/WeatherForecast';
    const params = {
      headers: {
          'Content-Type': 'application/json',
      }  
    };
    
    const res = http.get(url, params);
};