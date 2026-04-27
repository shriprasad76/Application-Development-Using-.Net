import { useState } from 'react';
import Login from './pages/Login';
import TeacherDashboard from './pages/TeacherDashboard';

function App() {
  const [user, setUser] = useState(null);

  const handleLogin = (userData) => {
    setUser(userData);
  };

  const handleLogout = () => {
    setUser(null);
    window.location.reload();
  };

  return (
    <div className="app-shell">
      <header>
        <h1>College Lab Evaluation System</h1>
      </header>
      <main>
        {user ? (
          <TeacherDashboard user={user} onLogout={handleLogout} />
        ) : (
          <Login onLogin={handleLogin} />
        )}
      </main>
    </div>
  );
}

export default App;
