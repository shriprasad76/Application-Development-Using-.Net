import { useState } from 'react';
import api from '../services/api';
import MarksEntry from './MarksEntry';

function TeacherDashboard({ user, onLogout }) {
  const [subjectName, setSubjectName] = useState('');
  const [batchName, setBatchName] = useState('');
  const [studentName, setStudentName] = useState('');
  const [studentBatchId, setStudentBatchId] = useState(1);
  const [message, setMessage] = useState('');

  const createSubject = async () => {
    if (!subjectName.trim()) {
      setMessage('Please enter a subject name.');
      return;
    }

    try {
      const response = await api.post('/teacher/subjects', { name: subjectName });
      setMessage(`Lab created: ${response.data.name}`);
      setSubjectName('');
    } catch (error) {
      console.error(error);
      setMessage('Unable to create lab.');
    }
  };

  const createBatch = async () => {
    if (!batchName.trim()) {
      setMessage('Please enter a batch name.');
      return;
    }

    try {
      const response = await api.post('/teacher/batches', { name: batchName });
      setMessage(`Batch created: ${response.data.name}`);
      setBatchName('');
    } catch (error) {
      console.error(error);
      setMessage('Unable to create batch.');
    }
  };

  const createStudent = async () => {
    if (!studentName.trim() || studentBatchId <= 0) {
      setMessage('Please provide student name and valid batch ID.');
      return;
    }

    try {
      const response = await api.post('/teacher/students', {
        name: studentName,
        batchId: studentBatchId
      });
      setMessage(`Student added: ${response.data.name}`);
      setStudentName('');
    } catch (error) {
      console.error(error);
      setMessage('Unable to add student.');
    }
  };

  return (
    <div className="teacher-dashboard">
      <div className="dashboard-header">
        <div>
          <h2>Welcome, {user.username}</h2>
          <p>Role: {user.role}</p>
        </div>
        <button type="button" onClick={onLogout}>
          Logout
        </button>
      </div>

      <section className="teacher-actions">
        <div className="card">
          <h3>Create Lab / Subject</h3>
          <input
            type="text"
            placeholder="Physics Lab"
            value={subjectName}
            onChange={(event) => setSubjectName(event.target.value)}
          />
          <button type="button" onClick={createSubject}>
            Create Lab
          </button>
        </div>

        <div className="card">
          <h3>Create Batch</h3>
          <input
            type="text"
            placeholder="Batch A"
            value={batchName}
            onChange={(event) => setBatchName(event.target.value)}
          />
          <button type="button" onClick={createBatch}>
            Create Batch
          </button>
        </div>

        <div className="card">
          <h3>Add Student</h3>
          <input
            type="text"
            placeholder="Student Name"
            value={studentName}
            onChange={(event) => setStudentName(event.target.value)}
          />
          <input
            type="number"
            min="1"
            placeholder="Batch ID"
            value={studentBatchId}
            onChange={(event) => setStudentBatchId(Number(event.target.value))}
          />
          <button type="button" onClick={createStudent}>
            Add Student
          </button>
        </div>
      </section>

      {message && <div className="message">{message}</div>}

      <section className="marks-section">
        <h3>Mark Entry</h3>
        <MarksEntry />
      </section>
    </div>
  );
}

export default TeacherDashboard;
