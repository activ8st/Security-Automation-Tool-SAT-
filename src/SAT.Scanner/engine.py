#!/usr/bin/env python3
"""
SAT Scanner Engine - Core Python Logic
Questo modulo gestisce l'esecuzione dei test DAST e l'interfaccia con il core .NET.
"""

import sys
import json
import argparse
from modules.web_analyzer import WebSecurityAuditor

def log_info(msg):
    print(f"[*] {msg}")

def log_error(msg):
    print(f"[!] ERROR: {msg}", file=sys.stderr)

def main():
    parser = argparse.ArgumentParser(description='SAT Scanner Engine')
    parser.add_argument('--target', required=True, help='Target URL or IP to scan')
    parser.add_argument('--format', default='json', choices=['json', 'text'], help='Output format')
    
    args = parser.parse_args()
    target = args.target

    log_info(f"Avvio scansione su: {target}")

    try:
        auditor = WebSecurityAuditor(target)
        results = auditor.run_all_tests()
        
        if args.format == 'json':
            # Output pulito per il core .NET
            print(json.dumps(results, indent=4))
        else:
            # Output leggibile per debug manuale
            for test, data in results.items():
                print(f"\n>> {test}: {data.get('status', 'N/A')}")
                
    except Exception as e:
        log_error(f"Eccezione durante la scansione: {str(e)}")
        sys.exit(1)

if __name__ == "__main__":
    main()
