import { useState, useEffect } from 'react'
import './App.css'

function App() {
  // 1. 定義狀態：用來儲存從後端抓回來的天氣陣列
  const [forecasts, setForecasts] = useState([]);
  const [loading, setLoading] = useState(true);

  // 2. 生命週期：當元件掛載時，執行抓取資料的動作
  useEffect(() => {
    fetch('http://localhost:5149/weatherforecast')
      .then(response => response.json())
      .then(data => {
        setForecasts(data);
        setLoading(false);
      })
      .catch(error => {
        console.error("抓取資料失敗:", error);
        setLoading(false);
      });
  }, []);

  return (
    <div className="App">
      <h1>天氣預報地基測試</h1>
      
      {loading ? (
        <p>載入中...</p>
      ) : (
        <table border="1" style={{ width: '100%', marginTop: '20px' }}>
          <thead>
            <tr>
              <th>日期</th>
              <th>攝氏 (C)</th>
              <th>華氏 (F)</th>
              <th>狀態</th>
            </tr>
          </thead>
          <tbody>
            {/* 3. 視覺化：將陣列資料對應 (Map) 到表格列 */}
            {forecasts.map((item, index) => (
              <tr key={index}>
                <td>{item.date}</td>
                <td>{item.temperatureC}°C</td>
                <td>{item.temperatureF}°F</td>
                <td>{item.summary}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  )
}

export default App