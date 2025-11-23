#!/usr/bin/env bash
set -euo pipefail

PROJECT="SauceDemo.BddFramework.csproj"
CONFIGURATION="Debug"
OUTPUT_DIR="TestReports"

while [[ $# -gt 0 ]]; do
  case "$1" in
    --configuration|-c)
      CONFIGURATION="$2"
      shift 2
      ;;
    --output|-o)
      OUTPUT_DIR="$2"
      shift 2
      ;;
    *)
      echo "Unknown argument: $1" >&2
      exit 1
      ;;
  esac
done

ASSEMBLY_PATH="SauceDemo.BddFramework/bin/${CONFIGURATION}/net8.0/SauceDemo.BddFramework.dll"
RESULTS_DIR="${OUTPUT_DIR}/TestResults"

# Ensure the LivingDoc CLI tool is available (uses local tool manifest)
dotnet tool restore

# Run tests and capture TRX + SpecFlow TestExecution.json output
mkdir -p "$RESULTS_DIR"
dotnet test "$PROJECT" \
  --configuration "$CONFIGURATION" \
  --logger "trx;LogFileName=TestResults.trx" \
  --results-directory "$RESULTS_DIR"

TEST_EXECUTION_FILE=$(find "$RESULTS_DIR" -name TestExecution.json | sort | tail -n 1)
if [[ -z "$TEST_EXECUTION_FILE" ]]; then
  echo "SpecFlow TestExecution.json not found under $RESULTS_DIR. Ensure SpecFlow.Plus.LivingDocPlugin is enabled." >&2
  exit 1
fi

# Generate LivingDoc HTML report
mkdir -p "$OUTPUT_DIR"
REPORT_PATH="$OUTPUT_DIR/LivingDoc.html"
livingdoc test-assembly "$ASSEMBLY_PATH" -t "$TEST_EXECUTION_FILE" -o "$REPORT_PATH"

echo "LivingDoc report generated at: $REPORT_PATH"
