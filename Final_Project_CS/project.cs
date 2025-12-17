using System;
using System.Collections.Generic;

// ====================== 1. PATIENT BASE CLASS ======================
public abstract class Patient
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }
    public string Gender { get; private set; }

    protected Patient(int id, string name, int age, string gender)
    {
        ID = id;
        Name = name;
        Age = age;
        Gender = gender;
    }

    public abstract double CalculateBill();

    public virtual void ShowDetails()
    {
        Console.WriteLine($"ID: {ID}, Name: {Name}, Age: {Age}, Gender: {Gender}");
    }
}

// ====================== 2. PATIENT TYPES ======================
public class IPD : Patient
{
    public double RoomCharges { get; private set; }

    public IPD(int id, string name, int age, string gender, double roomCharges = 1500)
        : base(id, name, age, gender)
    {
        RoomCharges = roomCharges;
    }

    public override double CalculateBill() => RoomCharges;

    public override void ShowDetails()
    {
        base.ShowDetails();
        Console.WriteLine($"Type: IPD, Room Charges: {RoomCharges:C}");
    }
}

public class OPD : Patient
{
    public double ConsultationFee { get; private set; }

    public OPD(int id, string name, int age, string gender, double consultationFee = 500)
        : base(id, name, age, gender)
    {
        ConsultationFee = consultationFee;
    }

    public override double CalculateBill() => ConsultationFee;

    public override void ShowDetails()
    {
        base.ShowDetails();
        Console.WriteLine($"Type: OPD, Consultation Fee: {ConsultationFee:C}");
    }
}

public class Emergency : Patient
{
    public double EmergencySurcharge { get; private set; }

    public Emergency(int id, string name, int age, string gender, double emergencySurcharge = 2000)
        : base(id, name, age, gender)
    {
        EmergencySurcharge = emergencySurcharge;
    }

    public override double CalculateBill() => EmergencySurcharge;

    public override void ShowDetails()
    {
        base.ShowDetails();
        Console.WriteLine($"Type: Emergency, Surcharge: {EmergencySurcharge:C}");
    }
}

// ====================== 3. BILLING STRATEGY ======================
public delegate double BillingStrategy(Patient patient);

public class Billing
{
    public double Standard(Patient patient) => patient.CalculateBill() * 1.10;
    public double Emergency(Patient patient) => patient.CalculateBill() * 1.20;
    public double Insurance(Patient patient) => patient.CalculateBill() * 0.70;
}

// ====================== 4. HOSPITAL MANAGEMENT ======================
public class Hospital
{
    public event Action<Patient> PatientAdmitted;
    public event Action<Patient, double> BillGenerated;

    private List<Patient> patients = new List<Patient>();

    public void Admit(Patient patient)
    {
        patients.Add(patient);
        Console.WriteLine($"\n {patient.Name} has been admitted.");
        PatientAdmitted?.Invoke(patient);
    }

    public void GenerateBill(Patient patient, BillingStrategy strategy)
    {
        double billAmount = strategy(patient);
        Console.WriteLine($"💰 Bill for {patient.Name}: {billAmount:C}");
        BillGenerated?.Invoke(patient, billAmount);
    }

    public void ShowAllPatients()
    {
        if (patients.Count == 0)
        {
            Console.WriteLine("\nNo patients admitted yet.");
            return;
        }

        Console.WriteLine("\n--- List of All Patients ---");
        foreach (var patient in patients)
        {
            patient.ShowDetails();
            Console.WriteLine("----------------------------");
        }
    }

    public Patient GetPatientByID(int id)
    {
        return patients.Find(p => p.ID == id);
    }
}

// ====================== 5. DEPARTMENTS ======================================================
public class Lab
{
    public void OnPatientAdmitted(Patient patient) => Console.WriteLine($"Lab: Prepare tests for {patient.Name}");
    public void OnBillGenerated(Patient patient, double amount) => Console.WriteLine($"Lab: Bill recorded for {patient.Name}: {amount:C}");
}

public class Pharmacy
{
    public void OnPatientAdmitted(Patient patient) => Console.WriteLine($"Pharmacy: Prepare medicines for {patient.Name}");
    public void OnBillGenerated(Patient patient, double amount) => Console.WriteLine($"Pharmacy: Bill recorded for {patient.Name}: {amount:C}");
}

public class BillingDept
{
    public void OnBillGenerated(Patient patient, double amount) => Console.WriteLine($"Billing: Processed bill for {patient.Name} = {amount:C}");
}

// ====================== 6. MAIN MENU ======================
class Program
{
    static void Main()
    {
        Hospital hospital = new Hospital();
        Billing billing = new Billing();

        Lab lab = new Lab();
        Pharmacy pharmacy = new Pharmacy();
        BillingDept billingDept = new BillingDept();

        hospital.PatientAdmitted += lab.OnPatientAdmitted;
        hospital.PatientAdmitted += pharmacy.OnPatientAdmitted;

        hospital.BillGenerated += lab.OnBillGenerated;
        hospital.BillGenerated += pharmacy.OnBillGenerated;
        hospital.BillGenerated += billingDept.OnBillGenerated;

        Console.WriteLine("=== Welcome to Hospital Patient Management System ===");

        int patientCounter = 1;

        while (true)
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. Admit New Patient");
            Console.WriteLine("2. View All Patients");
            Console.WriteLine("3. Generate Bill for a Patient");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Admit Patient
                    Console.WriteLine($"\n--- Enter details for Patient {patientCounter} ---");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Age: ");
                    int age = int.Parse(Console.ReadLine());
                    Console.Write("Gender (Male/Female): ");
                    string gender = Console.ReadLine();

                    Console.WriteLine("Select patient type: 1. OPD  2. IPD  3. Emergency");
                    int type = int.Parse(Console.ReadLine());

                    Patient patient = type switch
                    {
                        1 => new OPD(patientCounter, name, age, gender),
                        2 => new IPD(patientCounter, name, age, gender),
                        3 => new Emergency(patientCounter, name, age, gender),
                        _ => throw new Exception("Invalid type selected!")
                    };

                    hospital.Admit(patient);
                    patientCounter++;
                    break;

                case 2:
                    // View all patients
                    hospital.ShowAllPatients();
                    break;

                case 3:
                    // Generate bill
                    Console.Write("Enter Patient ID to generate bill: ");
                    int id = int.Parse(Console.ReadLine());
                    Patient selectedPatient = hospital.GetPatientByID(id);

                    if (selectedPatient == null)
                    {
                        Console.WriteLine("Patient not found!");
                        break;
                    }

                    Console.WriteLine("Select billing strategy: 1. Standard  2. Emergency  3. Insurance");
                    int billType = int.Parse(Console.ReadLine());
                    BillingStrategy strategy = billType switch
                    {
                        1 => billing.Standard,
                        2 => billing.Emergency,
                        3 => billing.Insurance,
                        _ => billing.Standard
                    };

                    hospital.GenerateBill(selectedPatient, strategy);
                    break;

                case 4:
                    Console.WriteLine("Exiting system. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option! Try again.");
                    break;
            }
        }
    }
}