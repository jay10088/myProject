import { useEffect, useState } from 'react'
import axios from 'axios'
import './App.css'

function App() {
  const [products, setProducts] = useState([])
  const [cart, setCart] = useState([])

  // 1. 取得菜單資料
  useEffect(() => {
    axios.get('http://localhost:5149/api/products')
      .then(res => setProducts(res.data))
      .catch(err => console.error("抓取失敗:", err))
  }, [])

  // 2. 加入購物車
  const addToCart = (p) => setCart([...cart, p])

  // 3. 送出訂單到後端資料庫
  const submitOrder = () => {
    const total = cart.reduce((sum, item) => sum + item.price, 0);

    // 發送 POST 請求給 .NET 後端
    axios.post('http://localhost:5149/api/orders', {
      TotalAmount: total
    })
    .then(res => {
      // res.data.message 是後端回傳的 "訂單已成功存入資料庫！"
      alert(res.data.message); 
      setCart([]); // 成功後清空購物車
    })
    .catch(err => {
      console.error("結帳失敗:", err);
      alert("結帳失敗，請檢查後端是否啟動");
    });
  };

  return (
    <div classname="app-container">
      <h1 classname="title">☕ 雲端點餐系統</h1>

      <div classname="product-section">
        <h2>今日菜單</h2>
        <div classname="product-grid">
          {products.map(p => (
            <div key={p.Id} classname="card">
              <h3>{p.name}</h3>
              <p>{p.Description}</p>
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <span classname="price">${p.price}</span>
                <button classname="btn-add" onClick={() => addToCart(p)}>加入</button>
              </div>
            </div>
          ))}
        </div>
      </div>

      <div classname="cart-section">
        <h2>🛒 我的訂單</h2>
        {cart.length === 0 ? <p>尚未點餐</p> : (
          <>
            <ul>
              {cart.map((item, idx) => (
                <li key={idx} style={{ marginBottom: '10px' }}>
                  {item.name} - <span style={{ color: '#e67e22' }}>${item.price}</span>
                </li>
              ))}
            </ul>
            <hr />
            <h3>總計: ${cart.reduce((sum, item) => sum + item.price, 0)}</h3>
            <button 
              classname="btn-checkout" 
              disabled={cart.length === 0}
              onClick={submitOrder}
            >
              確認下單
            </button>
          </>
        )}
      </div>
    </div>
  )
}

export default App