import { useEffect, useMemo, useState } from 'react';
import TableGrid from '../components/TableGrid';
import api from '../services/api';

const SUBJECTS = [
  { id: 1, name: 'Physics Lab' },
  { id: 2, name: 'Chemistry Lab' },
  { id: 3, name: 'Computer Lab' }
];

const DEFAULT_BATCH_ID = 1;

const initialEntry = (studentId) => ({
  studentId,
  lab1: 0,
  lab2: 0,
  lab3: 0,
  lab4: 0,
  lab5: 0,
  lab6: 0,
  lab7: 0,
  lab8: 0,
  lab9: 0,
  lab10: 0,
  lab11: 0,
  lab12: 0,
  cie1: 0,
  cie2: 0,
  cie3: 0,
  total: 0,
  finalOutOf50: 0
});

function MarksEntry() {
  const [batchId, setBatchId] = useState(DEFAULT_BATCH_ID);
  const [subjectId, setSubjectId] = useState(SUBJECTS[0].id);
  const [students, setStudents] = useState([]);
  const [marks, setMarks] = useState({});
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');

  useEffect(() => {
    async function loadStudents() {
      setLoading(true);
      try {
        const response = await api.get(`/students/by-batch/${batchId}`);
        setStudents(response.data);
        const map = {};
        response.data.forEach((student) => {
          map[student.id] = initialEntry(student.id);
        });
        setMarks(map);
      } catch (error) {
        console.error(error);
        setMessage('Unable to load students.');
      } finally {
        setLoading(false);
      }
    }

    loadStudents();
  }, [batchId]);

  const studentRows = useMemo(
    () => students.map((student) => ({
      studentId: student.id,
      name: student.name,
      ...marks[student.id]
    })),
    [students, marks]
  );

  const updateMark = (studentId, field, value) => {
    setMarks((prev) => {
      const updated = { ...prev[studentId], [field]: value };
      const total = calculateTotal(updated);
      return {
        ...prev,
        [studentId]: {
          ...updated,
          total,
          finalOutOf50: Math.round((total * 50) / 210)
        }
      };
    });
  };

  const calculateTotal = (entry) => {
    const labs = [
      entry.lab1,
      entry.lab2,
      entry.lab3,
      entry.lab4,
      entry.lab5,
      entry.lab6,
      entry.lab7,
      entry.lab8,
      entry.lab9,
      entry.lab10,
      entry.lab11,
      entry.lab12
    ];
    const cie = [entry.cie1, entry.cie2, entry.cie3];
    return labs.reduce((sum, value) => sum + Number(value), 0) + cie.reduce((sum, value) => sum + Number(value), 0);
  };

  const saveMarks = async () => {
    const entries = Object.values(marks).map((entry) => ({
      studentId: entry.studentId,
      lab1: Number(entry.lab1),
      lab2: Number(entry.lab2),
      lab3: Number(entry.lab3),
      lab4: Number(entry.lab4),
      lab5: Number(entry.lab5),
      lab6: Number(entry.lab6),
      lab7: Number(entry.lab7),
      lab8: Number(entry.lab8),
      lab9: Number(entry.lab9),
      lab10: Number(entry.lab10),
      lab11: Number(entry.lab11),
      lab12: Number(entry.lab12),
      cie1: Number(entry.cie1),
      cie2: Number(entry.cie2),
      cie3: Number(entry.cie3)
    }));

    setLoading(true);
    try {
      const response = await api.post('/marks/save', {
        subjectId,
        batchId,
        entries
      });
      setMessage(`Saved ${response.data.length} student mark records.`);
    } catch (error) {
      console.error(error);
      setMessage('Unable to save marks.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="marks-entry-page">
      <section className="controls">
        <label>
          Batch ID:
          <input type="number" value={batchId} min="1" onChange={(e) => setBatchId(Number(e.target.value))} />
        </label>
        <label>
          Subject:
          <select value={subjectId} onChange={(e) => setSubjectId(Number(e.target.value))}>
            {SUBJECTS.map((subject) => (
              <option key={subject.id} value={subject.id}>{subject.name}</option>
            ))}
          </select>
        </label>
      </section>

      {loading ? <p>Loading...</p> : null}
      {message ? <div className="message">{message}</div> : null}

      <TableGrid rows={studentRows} onCellChange={updateMark} />

      <div className="footer-actions">
        <button type="button" onClick={saveMarks} disabled={loading || students.length === 0}>
          Save All
        </button>
      </div>
    </div>
  );
}

export default MarksEntry;
